import React from 'react';

const ValuesTable = ({ nameValues }) => {

    let summedValues = nameValues.reduce((summedValues, currItem) => summedValues = summedValues + currItem.value, 0);

    return (
        <>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Value</th>
                    </tr>
                </thead>

                <tbody>
                    {(summedValues >= 11171) ?
                        nameValues.map((nameValue, index) => (
                            <tr key={index}>
                                <td>{nameValue.name}</td>
                                <td>{nameValue.value}</td>
                            </tr>
                        ))
                        :
                        <tr><td colSpan='2' className='text-center'>No data currently available.</td></tr>
                    }
                </tbody>
                {summedValues >= 11171 &&
                    <tfoot>
                        <tr>
                            <td className='font-weight-bold'>Total:</td>
                            <td>{summedValues}</td>
                        </tr>
                    </tfoot>
                }
            </table>
        </>
    );
}

export default ValuesTable;