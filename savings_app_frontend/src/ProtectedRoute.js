import { useLocation, Navigate, Outlet } from "react-router-dom";
//import useAuth from "../hooks/useAuth";
import useValidateJWT from "./Hooks/useValidateJWT";

const ProtectedRoute = ({roles }) => {
    //const { auth } = useAuth();
    const location = useLocation();

    const role = useValidateJWT();

    return (
        roles.includes(role)
            ? <Outlet />
            : role == null ?
                <Navigate to="/unauthorized" /> :
                <Navigate to={"/unauthorized" + (role == "seller" ? "buyer" : "seller")} />
            
    );
}

export default ProtectedRoute;