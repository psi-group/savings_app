import { useEffect, useState} from 'react';
import { useParams } from 'react-router-dom';


const RestaurantDetails = () => {

    const params = useParams()

    console.log(params.id);

    const [dataRestaurant, setRData] = useState(null);
    const [dataProducts, setPData] = useState(null);

    const imgURL = window.location.protocol + "//" + window.location.host + "/";

    useEffect(() => {
        fetch(`https://localhost:7183/api/restaurants/` + params.id)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(
                        `This is an HTTP error: The status is ${response.status}`
                    );
                }
                return response.json();
            })
            .then((actualData) => {
                fetch(`https://localhost:7183/api/products/`).then((res) => {
                    if (!res.ok) {
                        throw new Error(
                            `This is an HTTP error: The status is ${res.status}`
                        );
                    }
                    return res.json();
                }).then((pData) => {
                    setPData(pData);
                });
                setRData(actualData);
            })
            .catch((err) => {
                setRData(null);
                console.log(err);
            })
    }, [])

    const ProductList = (product) => {

        if (product.restaurantID == dataRestaurant.id) {
            return (
                <div>{product.name}</div>
            )
        }
       
    }
    if (dataRestaurant && dataProducts) {
        return (
            <>
                <h1 className="text-[35px] text-center mt-3 font-bold mb-5" >{dataRestaurant.name}</h1>
                <div>
                    <img className="w-[230px] h-[180px]" src={imgURL + dataRestaurant.image} alt={dataRestaurant.name} />
                    <div>
                        {dataProducts.map(product =>
                            <>{ProductList(product)}</>
                        )}
                    </div>
                </div>
            </>
        )
    } else {
        return <p> Loading... </p>
    }
}


export default RestaurantDetails