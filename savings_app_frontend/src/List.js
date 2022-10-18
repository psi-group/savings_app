import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import "./List.css";

export default class List extends Component {

    constructor(props) {
        super(props);
        this.state = { products: [] };
    }
    async populateProductsData() {

        console.log("SORTINAM" + this.props.sorting);


        let url = 'https://localhost:7183/api/products/filter?search=' + this.props.searched + "&order=" + this.props.sorting;

        this.props.filters.map((filter) => {
            url = url + "&filter=" + filter;
        })
        
        const response = await fetch(url);
        let data = await response.json();
        
        
        await this.setState({ products: data, loading: false });

    }

    componentDidMount() {
        this.populateProductsData();
    }

    async componentDidUpdate(prevProps)
    {
        if (this.props.searched !== prevProps.searched || this.props.filters != prevProps.filters || this.props.sorting != prevProps.sorting) {

            await this.populateProductsData();
        }
    }

    render() {
        return <div>
                <h4 className="text-sky-800 font-bold text-xl mt-3">Searching for: {this.props.searched}</h4>
            <ul>
                <div className="grid w-full grid-flow-row-dense grid-cols-3 gap-3 mt-10">
                    
                    {this.state.products.map((product) => (
                        <Link to={"/product/" + product.id} >
                        <div className = "border-2 border-sky-500 flex align-middle justify-center p-5 drop-shadow-xl rounded-xl bg-gradient-to-l from-sky-400 to-sky-900">
                            <li key={product.id} className="font-bold text-white text-xl  ">{product.name.toUpperCase()}</li>
                            </div>
                        </Link>
                    ))}
                </div>
                    
                </ul>
            </div>
    }
    
        
    
}