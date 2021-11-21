import React from 'react';

const Employee = ({ employees, removeEmployee, editEmployee }) => {
    return (
        <>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Value</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {employees.map((employee, index) => (
                        <tr key={index}>
                            <td>{employee.name}</td>
                            <td>{employee.value}</td>

                            <td>
                                <button className="btn btn-danger" onClick={() => removeEmployee(employee.id)}>
                                    Delete
                                </button>
                                <button className="btn btn-secondary" onClick={() => editEmployee(employee)}>
                                    Edit
                                </button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </>
    );
}

export default Employee;