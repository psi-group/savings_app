import { useLocation, Navigate, Outlet } from "react-router-dom";
//import useAuth from "../hooks/useAuth";
import useValidateJWT from "./Hooks/useValidateJWT";

const ProtectedRoute = () => {
    //const { auth } = useAuth();
    const location = useLocation();
    

    

    return (
        useValidateJWT()
            ? <Outlet />
            : <Navigate to="/login" />
    );
}

export default ProtectedRoute;