import React, { useState, useEffect } from 'react';

function EmployeeForm(props) {

    const initialState = {
        employeeName: props.isEditMode ? props.employee.name : '',
        employeeValue: props.isEditMode ? props.employee.value : 0
    }

    const [name, setName] = useState(initialState.employeeName);
    const [value, setValue] = useState(initialState.employeeValue);

    useEffect(() => {
        if (props.isEditMode && props.employee) {
            const { name: employeeName, value: employeeValue } = props.employee;
            setName(employeeName);
            setValue(employeeValue);
        }

    }, [props.isEditMode, props.employee])

    const handleNameChange = (e) => {
        e.preventDefault();
        setName(e.target.value);
    }

    const handleValueChange = (e) => {
        e.preventDefault();
        setValue(e.target.value);
    }

    const unsetValues = () => {
        setName('');
        setValue(0);
    }

    const handleSubmit = (e) => {

        e.preventDefault();

        const rawEmployee = {
            id: props.employee?.id,
            name,
            value
        };

        const body = JSON.stringify(rawEmployee);

        //TOOD check status codes
        if (!props.isEditMode) {

            fetch('/list',
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body

                })
                .then((resp) => resp.json())
                .then((employee) => props.insertSuccessful(employee))
                .then(() => unsetValues());

        } else {
            fetch('/list',
                {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body
                })
                .then(() => props.updateSuccessful(rawEmployee))
                .then(() => unsetValues());
        }
    }

    const afterSubmit = (e) => {
        e.preventDefault();
    }

    return (
        <form onSubmit={afterSubmit}>
            <h3>Manage Employee</h3>
            <hr />
            <div className="form-group">
                <label htmlFor="inputEmployeeName">Employee Name</label>
                <input
                    className='form-control'
                    placeholder='Employee Name'
                    value={name}
                    onChange={handleNameChange}
                    name='name'
                    id='inputEmployeeName'
                />
            </div>
            <div className="form-group">
                <label htmlFor="inputEmployeeValue">Value</label>
                <input
                    className='form-control'
                    type='number'
                    placeholder='0'
                    value={value}
                    onChange={handleValueChange}
                    name='value'
                    id='inputEmployeeValue'
                />
            </div>
            <div className="form-group">
                {props.isEditMode
                    ? <button className="btn btn-primary" onClick={handleSubmit}>
                        Save Changes
                    </button>
                    : <button className="btn btn-primary" onClick={handleSubmit}>
                        Add
                    </button>
                }
            </div>
        </form>
    )
}

export default EmployeeForm;
