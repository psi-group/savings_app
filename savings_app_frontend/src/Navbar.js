import { menuItems } from "./menuItems.js";


const Navbar = (props) => {


    function setSearch() {
        console.log("setinam");
        let value = document.getElementById('search').value;
        props.setSearchas(value);
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

                <label >Search:</label>
                <input type="text" id="search" />
                <button onClick={setSearch } >submit</button>

            </div>
            
        </>
        
    );
};

export default Navbar;