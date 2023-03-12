import { useEffect, useState} from 'react';
import { useParams, Link } from 'react-router-dom';
import StarRating from "./StarRating";
import Rating from '@mui/material/Rating';

const RestaurantDetails = () => {

    const params = useParams()

    console.log(params.id);

    const [dataRestaurant, setRData] = useState(null);
    const [dataProducts, setPData] = useState(null);

    const imgURL = dataRestaurant == null || dataRestaurant.imageUrl == null ?
        "https://savingsapp.blob.core.windows.net/userimages/profilePic.jpg" :
        dataRestaurant.imageUrl

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
                <Link to={"/product/" + product.id}>
                <div className="flex align-middle justify-center rounded-xl min-w-[200px]">
                  <li
                    key={product.id}
                    className="font-bold text-white text-xl flex flex-col items-center gap-3 border-2 hover:border-sky-400 p-3 border-sky-600 rounded-md shadow-sm shadow-sky-600"
                  >
                    <h1 className="font-extrabold text-transparent text-xl bg-clip-text bg-gradient-to-r from-sky-400 to-sky-700">
                      {product.name.toUpperCase()}
                    </h1>
                    <img
                      src={
                                    product.imageUrl == null ?
                                        "https://savingsapp.blob.core.windows.net/productimages/foodDefault.jpg" :
                                        product.imageUrl
                      }
                      className="w-60 h-60 rounded-md"
                    />
                    <div className="flex justify-between text-base flex-col items-center">
                    <p className="text-sky-600">
                        {product.category}
                      </p>
                      <p className="text-sky-600">
                        {product.price} Eur / {product.amountPerUnit}{" "}
                        {product.amountType}
                      </p>
                      <p className="text-sky-600 text-xs">
                        Max Quantity: {product.amountOfUnits}{" "}
                        {product.amountType}
                      </p>
                      
                    </div>
                  </li>
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
                    <img className=" rounded-lg align-middle w-[400px] h-[350px] border-sky-500 border-5 ml-auto mb-7 mr-auto"
                        src={
                            dataRestaurant.imageUrl == null ?
                            "https://savingsapp.blob.core.windows.net/userimages/profilePic.jpg" :
                                dataRestaurant.imageUrl
                    }
                        alt={dataRestaurant.name} />
                    <div className="ml-7 mb-6">
                        <h2 className=" font-bold text-[20px]">Description:</h2>
                        <p>{!dataRestaurant.description ? "-" : dataRestaurant.description }</p>
                    </div>
                   
                    <h3 className="text-[20px] font-bold ml-7">Offered products:</h3>
                    <div className='mx-10 grid gap-3 grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 mt-2 mb-20'>
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