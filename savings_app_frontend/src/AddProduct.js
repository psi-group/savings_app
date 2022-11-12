const AddProduct = () => {

    console.log("print");

    const handleSubmit = (e) => {

    }

    return(
        <div>
            <p>hello</p>
            <form onSubmit={handleSubmit}>
                <label>Product Name</label>
                <input type="text"></input>
                <label></label>
            </form>
        </div>
    );
}

export default AddProduct;