import React from 'react'

const Search = ({ handleSearch }) => {

    return (
        <>
            <input className="form-control" type="search" placeholder="Filter Employees..." aria-label="Search" onChange={(event) => handleSearch(event)} />
        </>
    );
}

export default Search;