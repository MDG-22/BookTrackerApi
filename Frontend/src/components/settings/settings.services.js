const API_URL = import.meta.env.VITE_BASE_SERVER_URL;

async function updateUser(id, token, updateData) {
  try {
    const response = await fetch(`${API_URL}/User/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(updateData),
    });

    const text = await response.text();
    const data = text ? JSON.parse(text) : null;

    if (!response.ok) {
      throw new Error(data?.message || 'Error actualizando el usuario');
    }

    return data;
  } catch (error) {
    console.error('updateUser error:', error);
    throw error;
  }
}

async function deleteUser(id, token) {
  try {
    const response = await fetch(`${API_URL}/User/${id}`, {
      method: 'DELETE',
      headers: { Authorization: `Bearer ${token}` },
    });

    if (!response.ok) {
      const text = await response.text();
      const data = text ? JSON.parse(text) : null;
      throw new Error(data?.message || 'Error eliminando la cuenta');
    }

    return true;
  } catch (error) {
    console.error('deleteUser error:', error);
    throw error;
  }
}

export { updateUser, deleteUser };
