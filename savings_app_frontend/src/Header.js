import Navbar from './Navbar';
import {Link} from 'react-router-dom';

const Header = (props) => {
    return (
        <header>
            <div className="nav-area ">
                <Link to="/" className="logo">
                    Logo
                </Link>
                <Navbar selector={props.selector} setSelector={props.setSelector} searchas={props.searchas} setSearchas={props.setSearchas} />
            </div>
        </header>
    );
};

export default Header;