    import { useState, useEffect, createContext } from "react";
    import { isTokenValid } from "./auth.helpers";
    import { warningToast } from "../../notifications/notifications";

    export const AuthenticationContext = createContext();

    const ROLE = {
    0: "Reader",
    1: "Admin",
    2: "SuperAdmin",
    };

    const getNumberRole = (role) => {
    // Convierte strings a números si vienen de localStorage
    const n = Number(role);
    return isNaN(n) ? 0 : n;
    };

    const tokenValue = localStorage.getItem("book-tracker-token");
    const usernameValue = localStorage.getItem("book-tracker-username");
    const idValue = localStorage.getItem("book-tracker-id");
    const userRole = getNumberRole(localStorage.getItem("book-tracker-role"));
    const profilePic = localStorage.getItem("book-tracker-profile-picture");

    export const AuthenticationContextProvider = ({ children }) => {
    const [token, setToken] = useState(tokenValue);
    const [username, setUsername] = useState(usernameValue);
    const [id, setId] = useState(idValue);
    const [role, setRole] = useState(userRole);
    const [profilePictureUrl, setProfilePictureUrl] = useState(profilePic);

    const handleUserLogin = (token, username, id, roleNumber, profilePictureUrl) => {
        localStorage.setItem("book-tracker-token", token);
        setToken(token);

        localStorage.setItem("book-tracker-username", username);
        setUsername(username);

        localStorage.setItem("book-tracker-id", id);
        setId(id);

        localStorage.setItem("book-tracker-role", roleNumber.toString());
        setRole(roleNumber);

        localStorage.setItem("book-tracker-profile-picture", profilePictureUrl);
        setProfilePictureUrl(profilePictureUrl);
    };

    const handleUserLogout = () => {
        localStorage.removeItem("book-tracker-token");
        localStorage.removeItem("book-tracker-username");
        localStorage.removeItem("book-tracker-id");
        localStorage.removeItem("book-tracker-role");
        localStorage.removeItem("book-tracker-profile-picture");

        setToken(null);
        setUsername(null);
        setId(null);
        setRole(0);
        setProfilePictureUrl(null);
    };

    const updateRole = (newRoleNumber) => {
        localStorage.setItem("book-tracker-role", newRoleNumber.toString());
        setRole(newRoleNumber);
    };

    const updateUsername = (newUsername) => {
        localStorage.setItem("book-tracker-username", newUsername);
        setUsername(newUsername);
    };

    const updateProfilePicture = (newProfilePictureUrl) => {
        localStorage.setItem("book-tracker-profile-picture", newProfilePictureUrl);
        setProfilePictureUrl(newProfilePictureUrl);
    };

    useEffect(() => {
        const interval = setInterval(() => {
        if (token && !isTokenValid(token)) {
            warningToast("Tu sesión ha expirado.");
            handleUserLogout();
        }
        }, 5000);

        return () => clearInterval(interval);
    }, [token]);

    return (
        <AuthenticationContext.Provider
        value={{
            token,
            username,
            id,
            role,
            profilePictureUrl,
            handleUserLogin,
            handleUserLogout,
            updateRole,
            updateUsername,
            updateProfilePicture,
        }}
        >
        {children}
        </AuthenticationContext.Provider>
    );
    };
