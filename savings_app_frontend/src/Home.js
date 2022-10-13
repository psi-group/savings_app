import React, { Component } from 'react';
import "./App.css";
import SearchAndDisplay from "./SearchAndDisplay";

function Home (props)  {


    const [content, setContent] = React.useState([]);
    const [loading, setLoading] = React.useState(true);

    const renderForecastsTable = (products) => {

        console.log(" start of rendering helper");
        //console.log(this.props.searchas + "wtf");

        return (
                <div className="main ml-20 mr-20">

                    <div>

                    <SearchAndDisplay searchas={props.searchas} products={products} cartItems={props.cartItems} setCartItems={props.setCartItems} />

                    </div>

                </div>
        );
        console.log("end of rendering helper");
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
            <h1 id="tabelLabel" className='text-center  text-bold text-5xl bg-gradient-to-r from-sky-300 to-sky-700 bg-clip-text text-transparent'>Savings app</h1>
            {contents}
        </div>
    );
    

    
}

export default Home;