import "./Filter.css";

const DropdownItem = (props) => {
  const handleChange = (event) => {
    if (event.target.checked) {
      event.preventDefault();
      props.setFilters((oldFilters) => [...oldFilters, props.children]);
    } else {
      props.setFilters((current) =>
        current.filter((filter) => filter !== props.children)
      );
    }
  };
  return (
    <a
      href="#"
      className="menu-item"
      onClick={() => props.goToMenu && props.setActiveMenu(props.goToMenu)}
    >
      {props.children}
      {props.activeMenu === "main" && <img src={props.arrow} />}
      {props.activeMenu !== "main" &&
        props.filters &&
        !props.filters.includes(props.children) && (
          <input
            type="checkbox"
            className="addFilterCheckbox"
            onChange={handleChange}
          ></input>
        )}
      {props.activeMenu !== "main" &&
        props.filters &&
        props.filters.includes(props.children) && (
          <input
            type="checkbox"
            className="addFilterCheckbox"
            checked
            onChange={handleChange}
          ></input>
        )}
    </a>
  );
};

export default DropdownItem;
