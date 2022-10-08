import "./ProductDetails.css";
import { useLocation, useParams } from 'react-router-dom';
import Popup from 'reactjs-popup';
import 'reactjs-popup/dist/index.css';
import React from 'react';




function ProductDetails() {

    const [content, setContent] = React.useState(null);
    const [product, setProduct] = React.useState(null);

    const [loading, setLoading] = React.useState(true);

    const [restId, setRestId] = React.useState(0);

    const [restaurant, setRestaurant] = React.useState(null);

    let { id } = useParams();


    console.log(id);

    //const [popUpMenu, setPopUpMenu] = React.useState(false);

    const selectTime =() =>{
        console.log("select");
    }


    


    /*React.useEffect(() => {

        console.log("start of effect");

        const url = "https://localhost:7183/api/products/id=1";

        const fetchData = async () => {
            try {
                const response = await fetch(url);
                const json = await response.json();
                //console.log(json);
                setProduct(json);
                console.log(product)

                const resp = await fetch("https://localhost:7183/api/restaurant/id=1");
                const json2 = await resp.json();

                setRestaurant(json2);

            } catch (error) {
                console.log("error", error);
            }
        };

       

        fetchData();


        //console.log(product);
    }, []);*/

    React.useEffect(() => {

        let ID = id;
        fetch('https://localhost:7183/api/products/id=' + ID)
            .then(res => res.json())
            .then(res => { setContent(res); console.log(res); return res; })
            .then(res => {
                console.log(res.restaurantID);
                const a = res.restaurantID;
                //console.log(content.restaurantId);
                let url = 'https://localhost:7183/api/restaurant/id=' + a;
                console.log(url);
                return fetch(url)
            })
            .then(res =>  res.json() )
            .then(res => { setRestaurant(res); console.log(res); })
            .then(res => { setLoading(false); })
            .catch(err => console.log(err));

        /*


        fetch('https://localhost:7183/api/restaurant/id=1')
            .then(res => res.json())
            .then(res => { setContent(res); console.log(res); })
            .catch(err => console.log(err));
            */
    }, []);

    
    console.log(id);
    
    /*function PopUpMenu() {
        return (
            <ul className="drop-down">
                <li>Menu-item-1</li>
                <li>Menu-item-2</li>
                <li>Menu-item-3</li>
            </ul>
        );
    }*/


    console.log("in details");

    /*
     * 
     * {restaurant.pickupTimes.map((pickUpTime) => (


                <div>
                    
                    <li key={product.id}>hello</li>
                </div>
            ))}

*/


    if (!loading) {
        return (

            <>

                <h2>Details of product #{id} sold by {restaurant.name}</h2>

                <h3>Available pickup Times</h3>

                {restaurant.pickupTimes.map((pickUpTime, index) => (


                    <div key={index }>

                        <li >{pickUpTime.availableDay} from {pickUpTime.startTime} to {pickUpTime.endTime}</li>
                    </div>
                ))}


            </>
        )
    }
    else {
        return <p> Loading... </p>
    }

    
}

export default ProductDetails;