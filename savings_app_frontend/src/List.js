import React, { Component } from 'react';
import "./List.css";

export default class List extends Component {

    constructor(props) {
        super(props);
        this.state = { products: [] };

    }
    async populateProductsData() {
        //console.log(this.state.searched);
        let data;
        if (this.props.searched === "")
            data = this.props.products;
        else {
            const response = await fetch('https://localhost:7183/api/products/filter?search=' + this.props.searched);
            data = await response.json();
            console.log(data);
        }
        
        await this.setState({ products: data, loading: false });
        console.log("finished populating");
        console.log(this.state.products);

    }

    componentDidMount() {
        console.log("mounted");
        this.populateProductsData();
    }

    async componentDidUpdate(prevProps)
    {
        console.log("update");
        if (this.props.searched !== prevProps.searched) {

            await this.populateProductsData();
            console.log(this.props.searched);
            //console.log(this.props.searched);
            console.log("priting");
            console.log(this.state.products);
        }
    }

    render() {
        return <div>
                <h4>Searching for {this.props.searched}</h4>
            <ul>
                
                    {this.state.products.map((product) => (

                        <div className="container">
                            <a href={"/product/" + product.id }><span></span></a>
                            <li key={product.id}>{product.name}</li>
                        </div>
                    ))}
                
                    
                </ul>
            </div>
    }
    
        
    
}