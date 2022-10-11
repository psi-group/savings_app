import { menuItems } from "./menuItems.js";
import { useNavigate } from "react-router-dom";


const Navbar = (props) => {

    const navigate = useNavigate();

    const handleClick = () => {
        navigate("/path/to/push");
    }

    function setSearch() {
        console.log("setinam searcha");
        let value = document.getElementById('search').value;
        props.setSearchas(value);

        var selected = document.getElementById('selector');
        var text = selected.options[selected.selectedIndex].text;

        var currentUrl = window.location.href === "https://localhost:3000/" ? window.location.href + "products" : window.location.href;

        
        if (currentUrl !== "https://localhost:3000/" + text.toLowerCase()) {

            navigate(text.toLowerCase());
        
            //window.location.replace("https://localhost:3000/" + text.toLowerCase());
        }
    }

    function setSelect() {
        console.log("setinam");
        var selected = document.getElementById('selector');
        var text = selected.options[selected.selectedIndex].text;
        props.setSelector(text);

        console.log("------------>" + text);
    }
    

    return (

        <>

            <nav>
                <ul className="menus">
                    {menuItems.map((menu, index) => {
                        return (
                            <li className="menu-items" key={index}>
                                <a href={menu.url}>{menu.title}</a>
                            </li>
                        );
                    })}
                </ul>
            </nav>

            <div>
                <select id="selector" onChange={setSelect}>
                    <option >Products</option>
                    <option >Restaurants</option>
                </select>
            </div>
            

            <div>

                <label >Search:</label>
                <input type="text" id="search" />
                <button onClick={setSearch } >search</button>

            </div>
            
        </>
        
    );
};

export default Navbar;