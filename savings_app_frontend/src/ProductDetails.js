import "./ProductDetails.css";
import { useParams } from "react-router-dom";
import imgSkeleton from "./img/productImageSkeleton.png";
import checkIcon from "./img/checkIcon.png";
import "reactjs-popup/dist/index.css";
import React from "react";

function ProductDetails(props) {

    const [product, setProduct] = React.useState(null);
  const [loading, setLoading] = React.useState(true);
  const [productName, setProductName] = React.useState("");
  const [productCategory, setProductCategory] = React.useState("");
  const [productDescription, setProductDescription] = React.useState("");
  const [imgURL, setImgURL] = React.useState("");
  const [restaurant, setRestaurant] = React.useState({});
  const [itemQuantity, setItemQuantity] = React.useState(1);
  const [itemPickupTime, setItemPickupTime] = React.useState("");
    const [errorVisible, setErrorVisible] = React.useState(false);
    const [pickups, setPickups] = React.useState({});

  let { id } = useParams();

  const capitalizeFirst = (str) => {
    return str.charAt(0).toUpperCase() + str.slice(1);
  };

  const setItemPickup = (e) => {
    setItemPickupTime(capitalizeFirst(e.target.value));
    setErrorVisible(false);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    if (itemPickupTime == "") {
      setErrorVisible(true);
    } else {
      props.addCartItem(capitalizeFirst(productName), itemPickupTime, parseInt(itemQuantity,10), imgURL);
    }
  };

    React.useEffect(() => {


        fetch("https://localhost:7183/api/pickups/byProductId/" + id)
            .then((res) => res.json())
            .then((data) => {
                
                let temp = [];
                data.map((pickup, index) => {
                    let availableDay = pickup.startTime.substring(0, 10);
                    let startTime = pickup.startTime.substring(14);
                    let endTime = pickup.endTime.substring(14);
                    temp.push({
                        availableDay: availableDay,
                        startTime: startTime,
                        endTime: endTime
                    });
                });
                setPickups(temp);
                console.log(temp);

            })
            .then((res) => {
                fetch("https://localhost:7183/api/products/" + id)
                    .then((res) => res.json())
                    .then((data) => {
                        setProduct(data);
                        setProductName(data.name);
                        setProductCategory(data.category);
                        setImgURL(data.pictureURL);
                        console.log(data);
                        const restaurantID = data.restaurantID;
                        let url = "https://localhost:7183/api/restaurants/" + restaurantID;
                        return fetch(url);
                    })
                    .then((res) => res.json())
                    .then((data) => {
                        setRestaurant(data);
                        setLoading(false);
                    })
                    .catch((err) => console.log(err));
            })

    
  }, []);

  if (!loading) {
    return (
      <div className="flex items-center w-full justify-center p-16 gap-10">
        {imgURL != "" ? (
          <img src={imgURL} className="w-3/12 h-3/12" />
        ) : (
          <img src={imgSkeleton} className="w-3/12 h-3/12" />
        )}

        <div className="flex flex-col self-start gap-3">
          <div>
            <h1 className="text-5xl text-sky-500">
              {capitalizeFirst(productName)}
            </h1>
            <h3 className="text-xl">{capitalizeFirst(productCategory)}</h3>
          </div>
          <div>
            <h2 className="text-2xl text-sky-500">Available pickup times</h2>

            <form className="flex gap-1 flex-col" onSubmit={handleSubmit}>
              {pickups.map((pickUpTime, index) => (
                <label
                  className="flex items-center rounded border-2 w-full border-sky-500"
                  name="pickUpTime"
                  key={index}
                >
                  <input
                    className="peer opacity-0 absolute "
                    type="radio"
                    value={`${pickUpTime.availableDay} from ${pickUpTime.startTime} to ${pickUpTime.endTime}`}
                    name="pickUpTime"
                    onChange={setItemPickup}
                  ></input>
                  <div className="pl-1 peer-hover:bg-sky-100 peer-checked:peer-hover:bg-sky-500 peer-checked:bg-sky-500 w-full peer-checked:text-white  flex justify-between items-center gap-2">
                    <p>
                      {capitalizeFirst(pickUpTime.availableDay)} from{" "}
                      {pickUpTime.startTime} to {pickUpTime.endTime}
                    </p>
                    <img
                      src={checkIcon}
                      className="invisible peer-checked:visible w-8 h-8"
                    />
                  </div>
                </label>
              ))}
              {errorVisible && (
                <h3 className="text-sky-500 text-lg font-bold">
                  Please select a pickup time
                </h3>
              )}
              <div className="flex justify-between gap-3 mt-3">
                <label for="quantitySelect" className="text-sky-500 text-xl">
                  Quantity:{" "}
                </label>
                <select
                  name="quantities"
                  id="quantitySelect"
                  className="w-full border-black border-2 rounded text-sky-500 text-lg"
                  onChange={(e) => setItemQuantity(e.target.value)}
                >
                  {[...Array(product.amountOfUnits)].map((value, index) => (
                    <option
                      value={index + 1}
                      key={index + 1}
                      className="text-right"
                    >
                      {(index + 1) * product.amountPerUnit + " " + product.amountType + "( " + (product.price * (index + 1)).toFixed(2) + " eur)"}
                    </option>
                  ))}
                </select>
              </div>
              <button
                type="submit"
                className="pl-10 pr-10 pt-3 pb-3 bg-gradient-to-r from-sky-400 to-blue-500 rounded-xl text-white text-lg active:shadow-lg active:font-bold"
              >
                Add To Cart
              </button>
            </form>
            <h2 className="text-sm text-center font-bold">
              Sold By {capitalizeFirst(restaurant.name)}
            </h2>
          </div>
        </div>
      </div>
    );
  } else {
    return <p> Loading... </p>;
  }
}

export default ProductDetails;
