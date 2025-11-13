import { useContext } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { AuthenticationContext } from '../services/auth/AuthContextProvider.jsx';
import { isTokenValid } from "../services/auth/auth.helpers.js";

const Protected = () => {
  const context = useContext(AuthenticationContext);

  if (!context) return <Navigate to="/login" replace />;

  const { token } = context;

  if (!isTokenValid(token)) {
    return <Navigate to="/login" replace />;
  } else {
    return <Outlet />;
  }
}

export default Protected;
