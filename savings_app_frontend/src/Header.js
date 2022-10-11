import Navbar from './Navbar';

const Header = (props) => {
    return (
        <header>
            <div className="nav-area ">
                <a href="/" className="logo">
                    Logo
                </a>
                <Navbar selector={props.selector} setSelector={props.setSelector} searchas={props.searchas} setSearchas={props.setSearchas} />
            </div>
        </header>
    );
};

export default Header;