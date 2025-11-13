const API_URL = import.meta.env.VITE_BASE_SERVER_URL;

async function updateLecture(token, lectureId, updateData) {
  try {
    const res = await fetch(`${API_URL}/Lecture/${lectureId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(updateData),
    });

    if (!res.ok) throw new Error('Error actualizando la lectura');

    const updatedLecture = await res.json();
    return updatedLecture;
  } catch (error) {
    console.error('updateLecture error:', error);
    throw error;
  }
}

async function deleteLecture(token, lectureId) {
  try {
    const res = await fetch(`${API_URL}/Lecture/${lectureId}`, {
      method: 'DELETE',
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    if (!res.ok) throw new Error('Error eliminando la lectura');

    return lectureId;
  } catch (error) {
    console.error('deleteLecture error:', error);
    throw error;
  }
}

async function fetchBook(bookId) {
  try {
    const res = await fetch(`${API_URL}/Book/${bookId}`);
    if (!res.ok) throw new Error('Error obteniendo el libro');

    const book = await res.json();
    return book; // debe tener Pages y CoverUrl
  } catch (error) {
    console.error('fetchBook error:', error);
    throw error;
  }
}

export { updateLecture, deleteLecture, fetchBook };
