import React, { useState, useEffect } from 'react';
import EmployeeForm from './EmployeeForm'
import Employee from './Employee'
//import update from 'immutability-helper';
import ValuesTable from './ValuesTable';
import Search from './Search'

function EmployeeList() {

    const [employees, setEmployees] = useState([]);
    const [isEditMode, setIsEditing] = useState(false);
    const [employeeToEdit, setEmployeeToEdit] = useState();
    const [nameValues, setNameValues] = useState([]);

    const [errorMessage, setErrorMessage] = useState();
    const [filteredEmployees, setFilteredEmployees] = useState([]);

    const getEmployees = () => {
        fetch('/employees')
            .then(response => {
                if (!response.ok)
                    throw new Error('Failed to fetch data.');
                else return response.json()
            })
            .then(data => {
                setEmployees(data)
                setFilteredEmployees(data)
            })
            .catch(error => setErrorMessage(error.message));
    }

    useEffect(() => {
        getEmployees()
    }, []);

    const removeEmployee = (id) => {
        fetch('/list/' + id,
            {
                method: 'DELETE'
            })
            .then(response => {
                if (!response.ok)
                    throw new Error('Failed to delete employee');
            })
            .then(() => {
                removeEmployeeFromState(id);
            })
            .then(() => {
                updateValuesList();
            })
            .catch(error => setErrorMessage(error.message));
    }

    const updateValuesList = () => {
        fetch('/list')
            .then(response => {
                if (!response.ok)
                    throw new Error('Failed to fetch data.');
                else return response.json()
            })
            .then(data => setNameValues(data))
            .catch(error => setErrorMessage(error.message));
    }

    const triggerUpdateView = (employee) => {
        setEmployeeToEdit(employee);
        setIsEditing(true);
    }

    const insertEmployeeSuccessful = (employee) => {
        setEmployees([...employees, employee]);
        setEmployeeToEdit(null);
    }

    const updateEmployeeSuccessful = (employee) => {

        // Since we have to update the values of all employees, we have to pull the list again from API
        // but, if we were to update an employee and nothing else, given that the response were to be
        // successful on the update, we would just replace the item in the state to avoid
        // another DB call, thus in large scale applications improving performance.

        // This is an example of how we would do that (using package immutability-helper):

        //const employeeToUpdate = employees.findIndex((emp) => emp.id === employee.id);
        //const updatedEmployees = update(employees, { $splice: [[employeeToUpdate, 1, employee]] })

        //setEmployees(updatedEmployees);

        getEmployees();
        updateValuesList();

        setIsEditing(false);
    }

    // Similar to the updateEmployeeSuccessful function, we just update the state if the response from server
    // is successful
    const removeEmployeeFromState = (id) => {
        let filteredArray = employees.filter(item => item.id !== id)
        setEmployees(filteredArray);

        let updateFilteredResults = filteredEmployees.filter(item => item.id !== id);
        setFilteredEmployees(updateFilteredResults);
    }

    const handleSearch = (e) => {

        const input = e.target.value;

        if (input !== '') {
            const filteredArray = employees.filter(employee => employee.name.toLowerCase().startsWith(input.toLowerCase()));
            setFilteredEmployees(filteredArray);
        } else {
            setFilteredEmployees(employees);
        }

        //if (input !== '') {
        //    const results = {employees.employees.filter((employee) => {
        //        return employee.name.toLowerCase().startsWith(input.toLowerCase());
        //    })

        //    setEmployees(results);
        //} else {
        //    setEmployees(employees);
        //}


    }

    return (
        <div>
            {
                errorMessage &&
                <p className="text-danger text-center">{errorMessage}</p>
            }
            <div className="col-3 ml-auto mt-3 mr-3">
                <Search
                    handleSearch={handleSearch}
                />
            </div>
            <div className="d-flex">
                <div className="col-3 mt-3">
                    <div className="col-12 p-3 border bg-light">
                        <EmployeeForm
                            employee={employeeToEdit}
                            isEditMode={isEditMode}
                            insertSuccessful={insertEmployeeSuccessful}
                            updateSuccessful={updateEmployeeSuccessful} />
                    </div>
                    <div className="col-12 p-3 mt-3">
                        <h3>Name-Values List</h3>
                        <ValuesTable
                            nameValues={nameValues} />
                    </div>
                </div>
                <div className="col-9 p-3 mt-3">
                    <h3>Employee List</h3>
                    <Employee
                        employees={filteredEmployees}
                        removeEmployee={removeEmployee}
                        editEmployee={triggerUpdateView} />
                </div>
            </div>
        </div>
    );
}

export default EmployeeList;