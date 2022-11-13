import Navbar from "./Navbar";
import { Link } from "react-router-dom";

const Header = (props) => {
  return (
    <header>
        <Navbar
          
          selector={props.selector}
          setSelector={props.setSelector}
          searchas={props.searchas}
          setSearchas={props.setSearchas}
          cartItems={props.cartItems}
          fullSum={props.fullSum}
        />
    </header>
  );
};

export default Header;
