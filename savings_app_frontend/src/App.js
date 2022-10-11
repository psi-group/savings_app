import React, { Component } from 'react';
import "./App.css";
import SearchAndDisplay from "./SearchAndDisplay";
import Main from "./Main.js";
import Header from "./Header.js";

const App = () => {

    const [searchas, setSearchas] = React.useState("");

    return (
        <div className="App">
            <Header searchas={searchas} setSearchas={setSearchas} />
            <Main searchas = {searchas }/>
        </div>
    );

}

export default App;