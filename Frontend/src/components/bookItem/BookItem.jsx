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
    bookId,
    bookTitle,
    authorName,
    authorId,
    startDate,
    finishDate
  } = lecture;

  const title = bookTitle || "Sin título";
  const author = authorName || "Desconocido";

  const [editStatus, setStatus] = useState(status || "");
  const [editRating, setRating] = useState(rating ?? "");
  const [editPageCount, setPageCount] = useState(pageCount != null ? pageCount.toString() : "");
  const [editStarted, setEditStarted] = useState(startDate ? startDate.slice(0,10) : "");
  const [editFinished, setEditFinished] = useState(finishDate ? finishDate.slice(0,10) : "");
  const [isEditing, setIsEditing] = useState(false);

  const [bookData, setBookData] = useState({ pages: 0, coverUrl: null });

  useEffect(() => {
    if (bookId) {
      fetchBook(bookId)
        .then(data => {
          setBookData({
            pages: data.pages,
            coverUrl: data.coverUrl
          });
        })
        .catch(err => console.error("Error fetching book:", err));
    }
  }, [bookId]);

  const handleClick = () => navigate(`/books/${bookId}`);
  const handleAuthorClick = () => authorId && navigate(`/authors/${authorId}`);

  const handleEdit = () => setIsEditing(true);
  const handleCloseEdit = () => setIsEditing(false);

  const handleEditStatus = (e) => {
    const newStatus = parseInt(e.target.value, 10);
    setStatus(newStatus);

    const today = new Date().toISOString().slice(0, 10);

    if (newStatus === 1 && !editStarted) {
      // Reading
      setEditStarted(today);
    }

    if (newStatus === 2) {
      // Read
      setPageCount(bookData.pages);
      if (!editFinished) setEditFinished(today);
      if (!editStarted) setEditStarted(today);
    }
  }

  const handleEditStartedDate = (e) => setEditStarted(e.target.value);
  const handleEditFinishedDate = (e) => setEditFinished(e.target.value);
  const handleEditRating = (e) => setRating(e.target.value !== "" ? parseInt(e.target.value) : "");
  
  const handleEditPageCount = (e) => {
    const value = e.target.value;
    setPageCount(value === "" ? "" : parseInt(value, 10));
  }


  const handleSaveEdit = async () => {

    if (editPageCount < 0) {
      errorToast("El número de páginas no puede ser negativo");
      return;
    }

    if (editPageCount > bookData.pages) {
      errorToast(`El número de páginas no puede superar las ${bookData.pages} del libro`);
      return;
    }

    try {
      const newLectureData = {
        Status: editStatus,
        Rating: editRating !== "" ? Number(editRating) : null,
        PageCount: editPageCount !== "" ? Number(editPageCount) : null,
        StartDate: editStarted ? new Date(editStarted).toISOString() : null,
        FinishDate: editFinished ? new Date(editFinished).toISOString() : null

      };

      const updated = await updateLecture(token, id, newLectureData);
      onUpdate(updated);
      setIsEditing(false);
    } catch (error) {
      console.error("Error al actualizar", error);
      errorToast("Error al actualizar la lectura");
    }
  }

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
            src={bookData.coverUrl || '/images/default-cover.png'}
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
            <option value={0}>{translate("Para leer")}</option>
            <option value={1}>{translate("Leyendo")}</option>
            <option value={2}>{translate("Leído")}</option>
          </FormSelect>
          ) : (
            translate(
              status === 0 ? "Para leer" :
              status === 1 ? "Leyendo"   :
              status === 2 ? "Leído"     :
              ""
  )
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
                  value={editPageCount}
                  onChange={handleEditPageCount}
                  disabled={editStatus !== 1}
                />

                <span> / {bookData.pages}</span>
              </>
            ) : (
              (status === 2 || status === 0) ? bookData.pages : `${pageCount} / ${bookData.pages}`
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
