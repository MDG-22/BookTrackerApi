import { useEffect, useState } from 'react';
import fetchBooks from './searchbar.services.js';
import { useNavigate } from 'react-router-dom';
import { useTranslate } from '../hooks/translation/UseTranslate.jsx';
import './searchBar.css';

const SearchBar = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredBooks, setFilteredBooks] = useState([]);
  const [books, setBooks] = useState([]);

  const navigate = useNavigate();
  const translate = useTranslate();

  // Normalización UNICODE
  const normalizeText = (text) =>
    text.normalize('NFD').replace(/[\u0300-\u036f]/g, '').toLowerCase();

  useEffect(() => {
    const loadBooks = async () => {
      try {
        const data = await fetchBooks();
        setBooks(data);
      } catch (error) {
        console.error('Error fetching books:', error);
      }
    };

    loadBooks();
  }, []);

  const handleSearchChange = (event) => {
    const term = event.target.value;
    setSearchTerm(term);

    const normalizedTerm = normalizeText(term);

    if (term.length > 0) {
      const found = books.filter(book => {
        const byTitle = normalizeText(book.title).includes(normalizedTerm);
        const byAuthor = normalizeText(book.authorName || "").includes(normalizedTerm);
        return byTitle || byAuthor;
      });

      setFilteredBooks(found.slice(0, 6));
    } else {
      setFilteredBooks([]);
    }
  };

  const handleClick = (id) => () => {
    navigate(`/books/${id}`);
    setSearchTerm("");
    setFilteredBooks([]);
  };

  return (
    <div className="search-wrapper">
      <input
        type="text"
        className="search-bar"
        placeholder={translate("search_placeholder")}
        value={searchTerm}
        onChange={handleSearchChange}
        autoComplete="off"
      />
      {filteredBooks.length > 0 && (
        <ol className="search-suggestions">
          {filteredBooks.map((book) => (
            <li key={book.id} className="suggestion-item" onClick={handleClick(book.id)}>
              <img
                src={book.coverUrl || "/default-cover.jpg"}
                alt={book.title}
                className="suggestion-cover"
              />
              <div className="suggestion-text">
                <div className="suggestion-title">{translate(book.title)}</div>
                <div className="suggestion-author">{book.authorName}</div>
              </div>
            </li>
          ))}
        </ol>
      )}
    </div>
  );
};

export default SearchBar;
