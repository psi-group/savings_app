import React, { Component } from 'react';
import "./App.css";
import SearchAndDisplay from "./Search/SearchAndDisplay";
import SortButton from "./Common/SortButton";

function Home (props)  {


    const [content, setContent] = React.useState([]);
    const [loading, setLoading] = React.useState(true);
    const [sorting, setSorting] = React.useState("by_id");

    const renderForecastsTable = (products) => {

        return (
                <div className="main ml-20 mr-20 mt-10">

                    <div >

                    <SortButton setSorting={setSorting}></SortButton>
                    
                    <SearchAndDisplay searchas={props.searchas} products={products} sorting={sorting } isValidUrl={props.isValidUrl}/>

                    </div>

                </div>
        );
    }

    React.useEffect(() => {

        console.log("useEffect");
        fetch('https://localhost:7183/api/products/')
            .then(res => res.json())
            .then(res => { setContent(res); setLoading(false) })
            .catch(err => console.log(err));
    }, []);

    
    let contents = loading
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : renderForecastsTable(content);


    return (
        <div>
            {contents}
        </div>
    );
    

    
}

export default Home;