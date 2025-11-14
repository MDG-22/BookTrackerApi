const API_URL = import.meta.env.VITE_BASE_SERVER_URL

export const fetchLectures = async (token) => {
    return fetch(`${API_URL}/Lecture`, {
        headers: {
            "Authorization": `Bearer ${token}`
        }
    })
        .then(res => {
            if (!res.ok) throw new Error('Error fetching books fetchlectures');
            return res.json()
        })
}

export const getBooks = async (token) => {
    return fetch(`${API_URL}/Book`, {
        headers: {
            "Authorization": `Bearer ${token}`
        },
        method: 'GET'
    })
        .then(res => {
            if (!res.ok) throw new Error('Error fetching books getbooks');
            return res.json()
        })
}

export const getRandomPopularBooks = async () => {
  const response = await fetch(`${API_URL}/Book`, {
    headers: {
      'Content-Type': 'application/json',
    },
    method: 'GET'
  });
  if (!response.ok) {
    throw new Error('Fallo el fetch popular books');
  }
  const books = await response.json();

  // Filtrar solo libros con id entre 1 y 40
  const filteredBooks = books.filter(book => book.id >= 1 && book.id <= 40);

  // Mezclar con Fisher-Yates
  for (let i = filteredBooks.length - 1; i > 0; i--) {
    const j = Math.floor(Math.random() * (i + 1));
    [filteredBooks[i], filteredBooks[j]] = [filteredBooks[j], filteredBooks[i]];
  }

  // Tomar los primeros 10
  return filteredBooks.slice(0, 10);
};
