const API_URL = import.meta.env.VITE_BASE_SERVER_URL;

const fetchUsers = async (token) => {
  return fetch(`${API_URL}/User/admin`, {
    headers: { "Authorization": `Bearer ${token}` }
  })
    .then(res => {
      if (!res.ok) throw new Error("Error fetching users");
      return res.json();
    });
};

async function updateUserRole(id, token, role) {
  try {
    const response = await fetch(`${API_URL}/User/${id}/role`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`
      },
      body: JSON.stringify({ role })
    });

    if (!response.ok) {
      const err = await response.json().catch(() => ({}));
      throw new Error(err.message || "Error actualizando el rol");
    }

    return true;
  } catch (error) {
    console.error("updateUserRole error:", error);
    throw error;
  }
}

async function deleteUser(id, token) {
  try {
    const response = await fetch(`${API_URL}/User/${id}`, {
      method: "DELETE",
      headers: { Authorization: `Bearer ${token}` }
    });

    if (!response.ok) {
      const err = await response.json().catch(() => ({}));
      throw new Error(err.message || "Error eliminando la cuenta");
    }

    return true;
  } catch (error) {
    console.error("deleteUser error:", error);
    throw error;
  }
}

export { fetchUsers, updateUserRole, deleteUser };
