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
      className="h-[50px] text-[20px] flex justify-between items-center transition-[background] duration-300 p-1 rounded-md hover:bg-sky-600 text-white border-b-[3px] last:border-0 border-white"
      onClick={() => props.goToMenu && props.setActiveMenu(props.goToMenu)}
    >
      {props.children}
      {props.activeMenu === "main" && <img src={props.arrow} className="w-[24px]  h-[24px] invert brightness-0"/>}
      {props.activeMenu !== "main" &&
        props.filters &&
        !props.filters.includes(props.children) && (
          <input
            type="checkbox"
            className="w-[25px] h-[25px] accent-black"
            onChange={handleChange}
          ></input>
        )}
      {props.activeMenu !== "main" &&
        props.filters &&
        props.filters.includes(props.children) && (
          <input
            type="checkbox"
            className="w-[25px] h-[25px] accent-black"
            checked
            onChange={handleChange}
          ></input>
        )}
    </a>
  );
};

export default DropdownItem;
