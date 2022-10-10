import React, { useEffect, useRef, useState } from 'react';
import { CSSTransition } from 'react-transition-group';
import "./Filter.css";
import arrow from './img/arrow.png';
import "./useFetchData.js";
import useFetchData from './useFetchData.js';



const Filter = (props) => {

    const [categories] = useFetchData("https://localhost:7183/api/categories");
    const [open, setOpen] = useState(false);

    return (

        <>
            <div class="filterContainer">
                <button type="button" className={open ? "dropdownButtonBlack": "dropdownButton"} onClick={() => setOpen(!open)}>Categories</button>
                {open && categories &&
                    <DropdownMenu categories={categories} setFilters={props.setFilters} filters={props.filters}/>
                }
            </div>
        </>

    );
};

const DropdownMenu = ({ categories, setFilters, filters }) => {
    const [activeMenu, setActiveMenu] = useState('main');
    const [menuHeight, setMenuHeight] = useState(null);
    const dropdownRef = useRef(null);

    useEffect(() => {
        setMenuHeight(dropdownRef.current?.firstChild.offsetHeight)
    }, [])

    function calcHeight(el) {
        const height = el.offsetHeight;
        setMenuHeight(height);
    }

    

    const DropdownItem = (props) => {
        const handleChange = event => {
            if (event.target.checked) {
                event.preventDefault();
                props.setFilters(oldFilters => [...oldFilters, props.children]);
            } else {
                props.setFilters((current) =>
                    current.filter((filter) => filter !== props.children)
                );
            }
        };
        return (
            <a href="#" className="menu-item" onClick={() => props.goToMenu && setActiveMenu(props.goToMenu)}>
                {props.children}
                {activeMenu === 'main' &&
                    <img src={arrow} />
                }
                {activeMenu !== 'main' && props.filters && !(props.filters.includes(props.children)) &&
                    <input type="checkbox" className="addFilterCheckbox" onChange={handleChange}></input>
                }
                {activeMenu !== 'main' && props.filters && props.filters.includes(props.children) &&
                    <input type="checkbox" className="addFilterCheckbox" checked onChange={handleChange}></input>   
                }
            </a>

        );
    }

    return (
        <div className="dropdown" style={{ height: menuHeight + 25 }} ref={dropdownRef}>

            <CSSTransition
                in={activeMenu === 'main'}
                unmountOnExit
                timeout={500}
                classNames="menu-primary"
                onEnter={calcHeight}
            >
                <div className="menu">
                    {categories.map((category, index) =>

                        <DropdownItem key={index} goToMenu={category.categoryName}>{category.categoryName.toUpperCase()}</DropdownItem>
                    )
                    }

                </div>
            </CSSTransition>


            {categories.map(category =>
                <CSSTransition
                    in={activeMenu === category.categoryName}
                    unmountOnExit
                    timeout={500}
                    classNames="menu-secondary"
                    onEnter={calcHeight}
                >
                    <div className="menu">
                        <div className="menu-item goBack" goToMenu="main">
                            <div className="goBackButton" onClick={() => setActiveMenu('main')}>
                                <img src={arrow} className="flippedArrow" />
                            </div>
                            <h3>{category.categoryName.toUpperCase()}</h3>
                        </div>
                        {category.subcategories.map(sub => {
                            return <DropdownItem key={sub} img={arrow} filters={filters} setFilters={setFilters}>{sub.toUpperCase()}</DropdownItem>;
                        })}


                    </div>
                </CSSTransition>
            )}

        </div>
    );
}

export default Filter;
