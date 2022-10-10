import React, { useState } from 'react'
import SearchBar from "./SearchBar";
import List from "./List";
import Filter from "./Filter";
import FilterDisplay from "./FilterDisplay";


const SearchAndDisplay = (props) => {

    const [searched, setSearched] = useState("");
    const [filters, setFilters] = useState([]);

    return (

        <div>

            <Filter filters={filters} setFilters={setFilters} />
            <FilterDisplay filters={filters} setFilters={setFilters} />
            <SearchBar searched={searched} setSearched={setSearched} />


            <List searched={searched} products={props.products} filters={filters} />
        
    </div>)
        
    


}

export default SearchAndDisplay;