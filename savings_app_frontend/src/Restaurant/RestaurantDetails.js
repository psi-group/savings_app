import { useEffect, useState} from 'react';
import { useParams, Link } from 'react-router-dom';
import StarRating from "./StarRating";
import Rating from '@mui/material/Rating';

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
                <Link to={"/product/" + product.id} >
                    <div className="border-2 mb-6 w-[25%] float-left ml-7 mt-6 align-middle border-sky-500 flex  justify-center p-5 drop-shadow-xl rounded-xl bg-gradient-to-l from-sky-400 to-sky-900">
                        <li key={product.id} className="font-bold text-white text-xl  list-none	">{product.name.toUpperCase()}</li>
                    </div>
                </Link>
            )
        }
       
    }
    if (dataRestaurant && dataProducts) {
        return (
            <>
                <h1 className="text-[35px] text-center mt-3 font-bold mb-5" >{dataRestaurant.name}</h1>
                <div>
                    <img className=" align-middle w-[400px] h-[350px] border-sky-500 border-5 ml-auto mb-7 mr-auto" src={"https://localhost:7183/userImg/" + dataRestaurant.id + ".jpg"} alt={dataRestaurant.name} />
                    <div className="w-[150px] ml-auto mr-auto">
                          <Rating name="half-rating" value={dataRestaurant.rating} precision={0.5}  size="large" />
                    </div>
                    <div className="ml-7 mb-6">
                        <h2 className=" font-bold text-[20px]">Description:</h2>
                        <p>{dataRestaurant.description}</p>
                    </div>
                   
                    <h3 className="text-[20px] font-bold ml-7">Offered products:</h3>
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