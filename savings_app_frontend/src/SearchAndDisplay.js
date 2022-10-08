import React, { useState } from 'react'
import SearchBar from "./SearchBar";
import List from "./List";


const SearchAndDisplay = (props) => {

    const [searched, setSearched] = useState("");
    const [filters, setFilters] = useState({});

    

    return (<div>
        <SearchBar searched={searched} setSearched={setSearched} />
        <Filter filters={filters} setFilters={setFilters }/>
        <List searched={searched} products={props.products} />
        
    </div>)
        
    


}

export default SearchAndDisplay;