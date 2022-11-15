import { MENU_ITEMS } from "../Constants";
import { Link } from "react-router-dom";
const MenuItems = () => {
  return (
    <ul className="flex text-black gap-3 items-center">
      {MENU_ITEMS.map((menu, index) => {
        return (
          <li key={index}>
            <Link className="text-black" to={menu.url}>
              {menu.title}
            </Link>
          </li>
        );
      })}
    </ul>
  );
};
export default MenuItems;
