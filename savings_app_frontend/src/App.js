import React, { Component } from 'react';
import "./App.css";
import SearchAndDisplay from "./SearchAndDisplay";
import Main from "./Main.js";
import Header from "./Header.js";

const App = () => {

    const [searchas, setSearchas] = React.useState("");
    const [selector, setSelector] = React.useState("");

    console.log("appas");

    return (
        <div className="App">
            <Header selector={selector} setSelector={setSelector} searchas={searchas} setSearchas={setSearchas} />
            <Main searchas={searchas} selector={selector} />
        </div>
    );

}

export default App;