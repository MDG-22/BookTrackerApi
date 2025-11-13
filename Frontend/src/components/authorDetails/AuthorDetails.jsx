import { useEffect, useState } from 'react'
import { useParams, useNavigate } from "react-router-dom";
import { Button, Card } from 'react-bootstrap';
import { useTranslate } from '../hooks/translation/UseTranslate.jsx';
import './authorDetails.css'

const AuthorDetails = () => {
  const translate = useTranslate();
  const navigate = useNavigate();
  const { id } = useParams();
  const [author, setAuthor] = useState(null);
  const [error, setError] = useState(false);

  useEffect(() => {
    fetch(`${import.meta.env.VITE_BASE_SERVER_URL}/Author/${id}`)
      .then(res => {
        if (!res.ok) throw new Error("Error de servidor");
        return res.json();
      })
      .then(data => setAuthor(data))
      .catch(err => {
        console.error("Error cargando autor", err);
        setError(true);
      });
  }, [id]);

  if (error) return <p>{translate("error_loading_author")}</p>;
  if (!author) return <p>{translate("loading_author")}</p>;

  const { name, description, books } = author;

  const handleExit = () => navigate("/");

  return (
    <div className='details-page'>
      <div className='author-body-container'>
        <div className='author-body'>
          <h2 className='author-name'>{name}</h2>
          <p className='author-description'>{description}</p>
        </div>

        <Button className="me-2 mt-3" onClick={handleExit}>
          {translate("back_home")}
        </Button>
      </div>
    </div>
  );
};

export default AuthorDetails;
