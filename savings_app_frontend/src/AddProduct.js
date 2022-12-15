import { useState } from "react";

const AddProduct = ({ setSnackOn, setSnackMessage }) => {
    console.log("print");

    const defaultImageSrc = "/images/foodDefault.png";

    const [name, setName] = useState("");
    const [category, setCategory] = useState("fruit");
    const [amountType, setAmountType] = useState("unit");
    const [amountOfUnits, setAmountOfUnits] = useState(0);
    const [amountPerUnit, setAmountPerUnit] = useState(0);
    const [price, setPrice] = useState(0);
    const [shelfLife, setShelfLife] = useState("");
    const [description, setDescription] = useState("");

    const [errorMsg, setErrMsg] = useState("");
    const [loading, setLoading] = useState(false);

    const [imageSrc, setImageSrc] = useState(defaultImageSrc);
    const [imageFile, setImageFile] = useState();

    const [pickupAmount, setPickupAmount] = useState(0);
    const [pickups, setPickups] = useState([]);

    function getId(token) {

        if (token != null)
            return JSON.parse(window.atob(token.split('.')[1]))["Id"]
        else
            return null;
    }

    const handleSubmit = (e) => {
        setLoading(true);

        e.preventDefault();

        console.log(pickups);

        const formData = new FormData();

        console.log(shelfLife);

        //let userAuth = { 'email': email, "password": password };

        formData.append("name", name);
        formData.append("category", category);
        formData.append("amountType", amountType);
        console.log(amountType);
        console.log(category);

        if (!isNaN(amountPerUnit))
            formData.append("amountPerUnit", amountPerUnit);

        if (!isNaN(amountOfUnits))
            formData.append("amountOfUnits", amountOfUnits);

        if (!isNaN(price))
            formData.append("price", price);
        formData.append("shelfLife", shelfLife);
        formData.append("description", description);

        formData.append("image", imageFile);
        formData.append("restaurantId", getId(localStorage.getItem("token")));

        console.log(formData.get("amountPerUnit"));

        fetch("https://localhost:7183/api/products", {
            method: "POST",
            headers: {
                "Access-Control-Allow-Origin": "*",
                "Authorization": "Bearer " + localStorage.getItem("token"),
            },
            body: formData,
        })
            .then(async (response) => {

                const json = await response.json();
                console.log(json);

                if (!response.ok) {
                    return Promise.reject(json);
                }

                const productId = json.id;

                console.log("id cia: " + productId);

                for (var i = 0; i < pickups.length; i++) {
                    console.log("i: " + i);
                    fetch("https://localhost:7183/api/pickups", {
                        method: "POST",
                        headers: {
                            "Access-Control-Allow-Origin": "*",
                            'Accept': 'application/json',
                            'Content-Type': 'application/json',
                            "Authorization" : "Bearer " + localStorage.getItem("token"),
                        },
                        body: JSON.stringify({
                            productId: productId,
                            startTime: pickups[i].startTime,
                            endTime: pickups[i].endTime
                        })
                    })
                        .catch(error => { setErrMsg(error.title); })
                }

                setLoading(false);
                setSnackMessage("Product and pickups created succesfully");
                setSnackOn(true);

            })
            .catch((error) => { setErrMsg(error.title); setLoading(false); });



    };

    const renderPickupInput = () => {
        let pickups = [];
        for (let i = 0; i < pickupAmount; i++) {
            pickups.push(
                <div className = "flex gap-2">
                    <label>Pickup Start Time</label>
                    <input
                        type="datetime-local"
                        className="focus:border-2 border-[1px] outline-none border-sky-500 rounded-xl"
                        onChange={(e) => handlePickupStart(i, e)}
                    ></input>
                    <br></br>
                </div>
            );

            pickups.push(

                <div className = "flex gap-2">
                    <label>Pickup End Time</label>
                    <input
                        type="datetime-local"
                        className="focus:border-2 border-[1px] outline-none border-sky-500 rounded-xl"
                        onChange={(e) => handlePickupEnd(i, e)}
                    ></input>
                </div>
            );
        }
        return pickups;
    };

    const addPickupTime = () => {
        setPickupAmount(pickupAmount + 1);
        let temp = pickups;
        temp.push("");
        setPickups(temp);
    };

    const handleImageChange = (e) => {
        console.log("change");
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = (x) => {
                setImageFile(imageFile);
                setImageSrc(x.target.result);
                console.log(x.target.result);
            };
            reader.readAsDataURL(imageFile);
        } else {
            setImageFile(null);
            setImageSrc(defaultImageSrc);
        }
    };

    const handlePickupStart = (i, e) => {
        let temp = pickups;
        console.log("pickup=" + pickups);
        temp[i] = { startTime: e.target.value, endTime: temp[i].endTime };
        console.log("temp=" + temp);
        setPickups(temp);
    };

    const handlePickupEnd = (i, e) => {
        let temp = pickups;
        console.log("pickup=" + pickups);
        temp[i] = { startTime: temp[i].startTime, endTime: e.target.value };
        console.log("temp=" + temp);
        setPickups(temp);
    };

    return (
        <div className="grid h-screen place-items-center sm:pt-20">
            <div className="text-[16px]  w-full sm:w-[700px] flex flex-col sm:border-sky-500 sm:border-2 px-6 py-3 rounded-xl shadow-xl">
                <p className="text-red-500">{errorMsg}</p>
                <h1 className="text-center font-bold  mb-3 text-3xl">Add Product</h1>
                <form
                    onSubmit={handleSubmit}
                    className="flex flex-col gap-3 [&>div>input]:border-[1px] [&>div>input]:outline-none [&>div>input]:border-sky-500 [&>div>input]:rounded-xl"
                >
                    <div className="flex flex-col">
                        <label>Product Name:</label>
                        <input
                            type="text"
                            className="focus:border-2"
                            onChange={(e) => setName(e.target.value)}
                        ></input>
                    </div>
                    <div className="flex flex-col">
                        <label>Category:</label>
                        <select onChange={(e) => setCategory(e.target.value)} className="focus:border-2 border-[1px] outline-none border-sky-500 rounded-xl" name="cars" id="cars">
                            <option value="fruit">Fruit</option>
                            <option value="vegetable">Vegetable</option>
                            <option value="grain">Grain</option>
                            <option value="protein">Protein</option>
                            <option value="dairy">Dairy</option>
                            <option value="snack">Snack</option>
                            <option value="dessert">Dessert</option>
                            <option value="meatDish">Meat dish</option>
                            <option value="salad">Salad</option>
                            <option value="bakery">Bakery</option>
                            <option value="drink">Drink</option>
                        </select>
                    </div>

                    <div className="flex flex-col sm:flex-row sm:justify-evenly gap-3">
                        <div className="flex flex-col sm:items-center">
                            <label >Amount Type</label>

                            <select onChange={(e) => setAmountType(e.target.value)} className="focus:border-2 border-[1px] outline-none border-sky-500 rounded-xl" name="cars" id="cars">
                                <option value="unit">Unit</option>
                                <option value="kilogram">Kilogram</option>
                                <option value="litre">Litre</option>

                            </select>


                        </div>
                        <div className="flex flex-col sm:items-center">
                            <label>Amount of Units</label>
                            <input
                                type="number"
                                className="focus:border-2 border-[1px] outline-none border-sky-500 rounded-xl"
                                onChange={(e) => setAmountOfUnits(parseInt(e.target.value))}
                            ></input>
                        </div>
                        <div className="flex flex-col sm:items-center">
                            <label>Amount Per Unit</label>
                            <input
                                type="number"
                                step="0.001"
                                className="focus:border-2 border-[1px] outline-none border-sky-500 rounded-xl"
                                onChange={(e) => { setAmountPerUnit(parseFloat(e.target.value)) ;  }}
                            ></input>
                        </div>
                    </div>
                    <div className="flex flex-col sm:flex-row sm:justify-evenly gap-3">
                        <div className="flex flex-col sm:items-center">
                            <label>Price</label>
                            <input
                                type="number"
                                step="0.01"
                                className="focus:border-2 border-[1px] outline-none border-sky-500 rounded-xl flex-1"
                                onChange={(e) => setPrice(parseFloat(e.target.value))}
                            ></input>
                        </div>
                        <div className="flex flex-col sm:items-center">
                            <label>Shelf Life</label>
                            <input
                                className="focus:border-2 border-[1px] outline-none border-sky-500 rounded-xl flex-1"
                                type="date"
                                onChange={(e) => setShelfLife(e.target.value)}
                            ></input>
                        </div>
                    </div>
                    <div className="flex flex-col h-28">
                        <label>Description:</label>
                        <input
                            type="text"
                            className=" focus:border-2 border-[1px] outline-none border-sky-500 rounded-xl flex-1"
                            onChange={(e) => setDescription(e.target.value)}
                        ></input>
                    </div>
                    <div className="flex flex-col">
                        <label>Product Picture:</label>
                        <input
                            accept="image/*"
                            type="file"
                            onChange={handleImageChange}
                        ></input>
                        <br></br>
                        <img
                            className="w-48 h-48 self-center border-2 border-sky-500 rounded-xl shadow-lg"
                            src={imageSrc}
                        ></img>
                    </div>

                    {pickupAmount > 0 && (
                        <div className="flex justify-between border-2 p-3 border-sky-500 rounded-lg">
                            {renderPickupInput()}{" "}
                        </div>
                    )}
                    <button
                        type="button"
                        className="border-2 border-sky-500 rounded-xl hover:text-white hover:bg-sky-500"
                        onClick={(e) => addPickupTime()}
                    >
                        Add Pickup Time
                    </button>
                    <button className="bg-sky-500 text-white rounded-xl font-bold hover:bg-sky-800 hover:text-white p-1"
                        disabled={loading}
                    >
                        <>

                            {loading && <div>
                                <i
                                    className="fa fa-refresh fa-spin"

                                />
                            </div>}
                        </>
                        Add Product
                    </button>
                </form>
            </div>
        </div>
    );
};

export default AddProduct;