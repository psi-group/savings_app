import { Link, useNavigate } from 'react-router-dom';
import "./Filter.css";
import React from 'react';



const Filter = (props) => {

    /*const handleChange = (e) => {
        e.preventDefault();
        props.setSearched(e.target.value);
    };

    const onSubmitTask = (e) => {
        e.preventDefault();
        props.setSearched(e.target.value);
    }
    */

    const categories = {
        freshFoods : [
            "meat",
            "vegetables",
            "fruits",
            "dairy",
            "eggs and grains"
        ],
        madeFoods : [
            "salads",
            "soup",
            "baked goods",
            "pasta",
            "meats"
        ]
    };

    React.useEffect(() => {

        
        
    }, []);

    const [filters, setFilters] = React.useState({});

    const freshFilter = () => {
        setFilters(filters + {});
    };

    const madeFilter = () => {
        setFilters(filters + ["made foods"]);
    };

    /*
     *
     * 
      <div>
                <input type="checkbox" onClick={freshFilter} />
                <label>Fresh foods</label>
                <input type="checkbox" onClick={madeFilter} />
                <label>Made foods</label>
            </div>

            <div className={`${filters ? " right-panel-active" : ""}`}>

            </div>
     */

    return (

        <>
           
        </>
        
    );
    

    


};

export default Filter;