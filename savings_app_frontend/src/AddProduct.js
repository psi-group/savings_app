import { useState } from 'react';


const AddProduct = () => {

    console.log("print");

    const defaultImageSrc = '/images/foodDefault.png';

    const [name, setName] = useState("");
    const [category, setCategory] = useState("");
    const [amountType, setAmountType] = useState("");
    const [amountOfUnits, setAmountOfUnits] = useState(0);
    const [amountPerUnit, setAmountPerUnit] = useState(0);
    const [price, setPrice] = useState(0);
    const [shelfLife, setShelfLife] = useState("");
    const [description, setDescription] = useState("");

    const [imageSrc, setImageSrc] = useState(defaultImageSrc);
    const [imageFile, setImageFile] = useState();

    const [pickupAmount, setPickupAmount] = useState(0);
    const [pickups, setPickups] = useState([]);

    const handleSubmit = (e) => {
        e.preventDefault();

        console.log(pickups);

        const formData = new FormData();

        //let userAuth = { 'email': email, "password": password };

        formData.append('name', name);
        formData.append('category', category);
        formData.append('amountType', amountType);
        formData.append('amountPerUnit', amountPerUnit);

        
        formData.append('amountOfUnits', amountOfUnits);
        formData.append('price', price);
        formData.append('shelfLife', shelfLife);
        formData.append('description', description);

        formData.append('imageFile', imageFile);

        //formData.append('pickups', JSON.stringify(pickups));

        
        for (var i = 0; i < pickups.length; i++) {
            formData.append(`pickups[${i}][startTime]`, pickups[i].startTime);
            formData.append(`pickups[${i}][endTime]`, pickups[i].endTime);
        }

        

        

        fetch("https://localhost:7183/api/products",
            {
                method: "POST",
                headers: {
                    "Access-Control-Allow-Origin": "*",
                    "Authorization": "Bearer " + localStorage.getItem("token")
                },
                body: formData
            }).
            then(async response => {

                const isJson = response.headers.get('content-type')?.includes('application/json');

                console.log(isJson);

                console.log(response);
                
            }).catch(error => {

                
            });
    }

    const renderPickupInput = () => {
        let pickups = [];
        for (let i = 0; i < pickupAmount; i++) {
            pickups.push(<label>Pickup Start Time</label>)
            pickups.push(<input className=" mb-2 ml-[10px] border-2" onChange={(e) => handlePickupStart(i, e)}></input>);
            pickups.push(<label>Pickup End Time</label>)
            pickups.push(<input className=" mb-2 ml-[10px] border-2" onChange={(e) => handlePickupEnd(i, e)}></input>);
            pickups.push(<br></br>);
        }
        return pickups;
    };

    const addPickupTime = () => {
        setPickupAmount(pickupAmount + 1);
        let temp = pickups;
        temp.push("");
        setPickups(temp);
    }

    const handleImageChange = (e) => {
        console.log("change");
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setImageFile(imageFile);
                setImageSrc(x.target.result);
                console.log(x.target.result);
            }
            reader.readAsDataURL(imageFile);
        }
        else {
            setImageFile(null);
            setImageSrc(defaultImageSrc);
        }
    }

    const handlePickupStart = (i, e) => {
        let temp = pickups;
        console.log("pickup=" + pickups);
        temp[i] = { startTime: e.target.value, endTime: temp[i].endTime };
        console.log("temp=" + temp);
        setPickups(temp);
    }

    const handlePickupEnd = (i, e) => {
        let temp = pickups;
        console.log("pickup=" + pickups);
        temp[i] = { startTime: temp[i].startTime, endTime: e.target.value };
        console.log("temp=" + temp);
        setPickups(temp);
    }

    return(
        <div>
            <form onSubmit={handleSubmit}>
                <label>Product Name</label>
                <input type="text" onChange={(e) => setName(e.target.value) }></input>
                <label>Category</label>
                <input type="text" onChange={(e) => setCategory(e.target.value)}></input><br></br>
                <label>Amount Type</label><br></br>
                <input type="text" onChange={(e) => setAmountType(e.target.value)}></input><br></br>
                <label>Amount of Units</label><br></br>
                <input type="text" onChange={(e) => setAmountOfUnits(parseInt(e.target.value))}></input><br></br>
                <label>Amount Per Unit</label><br></br>
                <input type="text" onChange={(e) => setAmountPerUnit(parseFloat(e.target.value))}></input><br></br>
                <label>Price</label><br></br>
                <input type="text" onChange={(e) => setPrice(parseFloat(e.target.value))}></input><br></br>
                <label>Shelf Life</label><br></br>
                <input type="text" onChange={(e) => setShelfLife(e.target.value)}></input><br></br>
                <label>Description</label><br></br>
                <input type="text" onChange={(e) => setDescription(e.target.value)}></input><br></br>

                <label>Product Picture:</label>
                <input className=" mb-2 ml-[10px] border-2" accept="image/*" type="file"  onChange={handleImageChange} ></input><br></br>
                <img src={imageSrc} ></img>

                <button type="button" className=" mb-2 ml-[10px] border-2" onClick={(e) => addPickupTime()}>Add Pickup Time</button><br></br>
                {renderPickupInput()}
                <button>Add Product</button>
            </form>
        </div>
    );
}

export default AddProduct;