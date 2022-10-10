import React, { Component } from 'react';
import "./List.css";

export default class List extends Component {

    constructor(props) {
        super(props);
        this.state = { products: [] };
    }
    async populateProductsData() {
        console.log(this.props.filters);
        console.log("populating");
        //console.log(this.state.searched);
        let url = 'https://localhost:7183/api/products/filter?search=' + this.props.searched;

        this.props.filters.map((filter) => {
            url = url + "&filter=" + filter;
        })

        console.log(url);
        
        const response = await fetch(url);
        let data = await response.json();
        console.log(data);
        
        
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
        console.log(this.props.filters);
        console.log("update");
        if (this.props.searched !== prevProps.searched || this.props.filters != prevProps.filters) {

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