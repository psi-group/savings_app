import React from "react";

const Card = ({ product }) => {
    // destructuring props
    return (
        <>
            <div className="container-fluid">
                <div className="row justify-content-center">
                            <div
                                className="col-md-4 col-sm-6 card my-3 py-3 border-0"
                                key={product.id}
                            >
                                <div className="card-img-top text-center">
                            <img src={product.imageUrl == null ?
                                "https://savingsapp.blob.core.windows.net/productimages/foodDefault.jpg" :
                                product.imageUrl} alt={product.name} className="photo w-75" />
                                </div>
                                <div className="card-body">
                                    <div className="card-title fw-bold fs-4">
                                        {product.name} &nbsp;&nbsp;&nbsp;&nbsp;--&nbsp;&nbsp;
                                        {product.shelfLife}
                                    </div>
                                    <div className="card-text">{product.description}</div>
                                </div>
                            </div>
                </div>
            </div>
        </>
    );
};

export default Card;