import { useState, useEffect, useContext } from 'react';
import { ListGroupItem, Row, Col, CardImg, Button, FormGroup, FormControl, FormSelect } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { Trash3, CheckLg, XLg, PencilSquare } from 'react-bootstrap-icons';
import { updateLecture, deleteLecture, fetchBook } from './bookitem.services.js';
import { useTranslate } from '../hooks/translation/UseTranslate';
import { AuthenticationContext } from '../services/auth/AuthContextProvider.jsx';
import './bookItem.css';

const BookItem = ({ lecture, onUpdate, onDelete }) => {
  const navigate = useNavigate();
  const translate = useTranslate();
  const { token } = useContext(AuthenticationContext);

  const {
    id,
    rating,
    status,
    pageCount,
    BookId,
    BookTitle,
    AuthorName,
    AuthorId,
    StartDate,
    FinishDate
  } = lecture;

  const title = BookTitle || "Sin título";
  const authorName = AuthorName || "Desconocido";

  // Estados de edición
  const [editStatus, setStatus] = useState(status || "");
  const [editRating, setRating] = useState(rating ?? "");
  const [editPageCount, setPageCount] = useState(pageCount ?? 0);
  const [editStarted, setEditStarted] = useState(StartDate ? StartDate.slice(0,10) : "");
  const [editFinished, setEditFinished] = useState(FinishDate ? FinishDate.slice(0,10) : "");
  const [isEditing, setIsEditing] = useState(false);

  // Estados del book
  const [bookData, setBookData] = useState({ pages: 0, coverUrl: null });

  useEffect(() => {
    if (BookId) {
      fetchBook(BookId)
        .then(data => {
          setBookData({
            pages: data.Pages,
            coverUrl: data.CoverUrl
          });
        })
        .catch(err => console.error("Error fetching book:", err));
    }
  }, [BookId]);

  // Navegación
  const handleClick = () => navigate(`/books/${BookId}`);
  const handleAuthorClick = () => AuthorId && navigate(`/authors/${AuthorId}`);

  // Edit handlers
  const handleEdit = () => setIsEditing(true);
  const handleCloseEdit = () => setIsEditing(false);

  const handleEditStatus = (e) => {
    const newStatus = e.target.value;
    setStatus(newStatus);

    const today = new Date().toISOString().slice(0, 10);
    if (newStatus === 'Leyendo' && !editStarted) setEditStarted(today);
    if (newStatus === 'Leído') {
      setPageCount(bookData.pages);
      if (!editFinished) setEditFinished(today);
      if (!editStarted) setEditStarted(today);
    }
  }

  const handleEditStartedDate = (e) => setEditStarted(e.target.value);
  const handleEditFinishedDate = (e) => setEditFinished(e.target.value);
  const handleEditRating = (e) => setRating(e.target.value !== "" ? parseInt(e.target.value) : "");
  const handleEditPageCount = (e) => setPageCount(e.target.value !== "" ? parseInt(e.target.value) : 0);

  // Guardar cambios
  const handleSaveEdit = async () => {
    try {
      const updated = await updateLecture(token, id, {
        Status: editStatus || null,
        Rating: editRating !== "" ? editRating : null,
        PageCount: editPageCount !== "" ? editPageCount : null,
        StartDate: editStarted || null,
        FinishDate: editFinished || null
      });
      onUpdate(updated);
      setIsEditing(false);
    } catch (error) {
      console.error("Error al actualizar", error);
    }
  }

  // Eliminar lectura
  const handleDelete = async () => {
    try {
      await deleteLecture(token, id);
      onDelete(id);
    } catch (error) {
      console.error("Error al eliminar", error);
    }
  }

  return (
    <ListGroupItem>
      <Row>
        <Col xs={1} className='list-item-cover'>
          <CardImg
            src={bookData.coverUrl || '/images/default-cover.png'} // portada por defecto
            className='clickable cover'
            onClick={handleClick}
          />
        </Col>

        <Col xs={3}>
          <span className='clickable list-item-title' onClick={handleClick}>{translate(title)}</span>
          <br />
          <span className='clickable list-item-author' onClick={handleAuthorClick}>{translate(authorName)}</span>
        </Col>

        <Col xs={2}>
          {isEditing ? (
            <FormSelect value={editStatus || ""} onChange={handleEditStatus}>
              <option value=""></option>
              <option value="Para leer">{translate("Para leer")}</option>
              <option value="Leyendo">{translate("Leyendo")}</option>
              <option value="Leído">{translate("Leído")}</option>
            </FormSelect>
          ) : (
            translate(status)
          )}
        </Col>

        <Col xs={1} className='list-item-rating'>
          {isEditing ? (
            <FormSelect value={editRating !== "" ? editRating : ""} onChange={handleEditRating}>
              <option value=""></option>
              {[1,2,3,4,5].map(n => <option key={n} value={n}>{n}</option>)}
            </FormSelect>
          ) : rating}
        </Col>

        <Col xs={2}>
          <FormGroup className="d-flex align-items-center">
            {isEditing ? (
              <>
                <FormControl
                  type="number"
                  size="sm"
                  min="0"
                  max={bookData.pages}
                  className="me-1"
                  style={{ width: '50px' }}
                  step="1"
                  value={editPageCount ?? 0}
                  onChange={handleEditPageCount}
                  disabled={editStatus !== 'Leyendo'}
                />
                <span> / {bookData.pages}</span>
              </>
            ) : (
              (status === 'Leído' || status === 'Para leer') ? bookData.pages : `${pageCount} / ${bookData.pages}`
            )}
          </FormGroup>
        </Col>

        <Col xs={2} className='list-item-date'>
          {isEditing ? (
            <>
              {translate("start")}
              <FormControl type='date' size='sm' value={editStarted} onChange={handleEditStartedDate} />
              <br />
              {translate("end")}
              <FormControl type='date' size='sm' value={editFinished} onChange={handleEditFinishedDate} />
            </>
          ) : (
            <div className='lecture-date'>
              {translate("start")}<div>{editStarted || '-'}</div>
              <br />
              {translate("end")}<div>{editFinished || '-'}</div>
            </div>
          )}
        </Col>

        <Col xs={1} className='list-item-edit'>
          {isEditing ? (
            <>
              <Button variant='secondary' className='edit-boton' onClick={handleCloseEdit}><XLg size={20} /></Button>
              <Button variant='success' className='edit-boton' onClick={handleSaveEdit}><CheckLg size={20} /></Button>
              <Button variant='danger' className='edit-boton' onClick={handleDelete}><Trash3 size={20} /></Button>
            </>
          ) : (
            <Button variant='secondary' className='edit-boton' onClick={handleEdit}><PencilSquare size={20} /></Button>
          )}
        </Col>
      </Row>
    </ListGroupItem>
  );
}

export default BookItem;
