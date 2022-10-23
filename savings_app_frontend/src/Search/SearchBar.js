import { Link, useNavigate } from 'react-router-dom';


const SearchBar = (props) => {

    const handleChange = (e) => {
        e.preventDefault();
        props.setSearched(e.target.value);
    };

    const onSubmitTask = (e) => {
        e.preventDefault();
        props.setSearched(e.target.value);
    }
    
    

    return <div>

        <input
            type="search"
            placeholder="Search here"
            onChange={handleChange}
            value={props.searched} />

        

    </div>


};

export default SearchBar;