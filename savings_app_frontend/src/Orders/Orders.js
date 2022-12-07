import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import SearchAndDisplay from "../Search/SearchAndDisplay";
import { useNavigate } from "react-router-dom";

export default class Orders extends Component {

    constructor(props) {
        super(props);
        this.state = { orders: [], loading: true };
        this.getOrders = this.getOrders.bind(this);
    }


    


    async getOrders() {


        let value = getId(localStorage.getItem('token'))

        const response = await fetch('https://localhost:7183/api/orders/byBuyerId/' + value);
        const data = await response.json();

        this.setState({orders: data, loading : false});
        console.log(data);

    }

    

    render() {


        let contents = 
           <table className='ml-auto mr-auto'>
                <thead className="border-1 border-solid">
                <tr >
                    <th>Date</th>
                    <th>Seller</th>
                    <th>Status</th>
                    <th>Price</th>
                    <th>Action</th>
                </tr>
            </thead>
                <tbody className="u-table-alt-palette-1-light-3 u-table-body u-white u-table-body-1">
              <tr>
                    <td className='w-[200px]'>2012-12-12 12:22</td>
                    <td className='w-[200px]'> <Link href='restLink'> restrNmae</Link></td>
                    <td className='w-[150px]'>Complete</td>
                    <td className='w-[100px]'>$???</td>
                    <td className='w-[50px]'><button className='border-1 border-solid'>VIEW</button></td>
              </tr>
            </tbody>
          </table>

        return (
            <>
                <h1 className='text-xl text-center'>Your orders history</h1>
                {contents}
            </>
           
        )
    }
}



function getId(token) {
    console.log(JSON.parse(window.atob(token.split(".")[1])));
    return JSON.parse(window.atob(token.split(".")[1]))[
        "Id"
    ];
}