const API_URL = import.meta.env.VITE_BASE_SERVER_URL;

const fetchLogin = async (email, password) => {
  const response = await fetch(`${API_URL}/Auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
  });

  const data = await response.json();

  if (!response.ok) {
    const error = new Error(data.message || "Error al iniciar sesión");
    error.status = response.status;
    throw error;
  }

  return {
    ...data,
    role: Number(data.role),
  };
};

export default fetchLogin;
