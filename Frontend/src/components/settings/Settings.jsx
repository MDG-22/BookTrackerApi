import { useState, useContext } from 'react';
import { Modal, Button, Form, Spinner } from 'react-bootstrap';
import { AuthenticationContext } from '../services/auth/AuthContextProvider.jsx';
import { successToast, errorToast } from '../notifications/notifications.js';
import { updateUser, deleteUser } from './settings.services.js';
import { useNavigate } from 'react-router-dom';
import './Settings.css';
import { useTranslate } from '../hooks/translation/UseTranslate.jsx';
import { validateEmail, validatePassword } from '../auth/auth.services.js';

function Settings() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const { id, token, handleUserLogout } = useContext(AuthenticationContext);
  const navigate = useNavigate();
  const translate = useTranslate();

  const handleSaveChanges = async () => {
    if (!email && !password) {
      return errorToast(translate('no_data_to_update') || 'No ingresaste ningún dato para actualizar');
    }

    if (email && !validateEmail(email)) {
      return errorToast("El mail ingresado es inválido");
    }

    if (password && !validatePassword(password, 6, 12, true, true)) {
      return errorToast("La contraseña debe tener entre 6 y 12 caracteres. Al menos 1 mayúscula y 1 número");
    }

    const updateData = {};
    if (email) updateData.email = email;
    if (password) updateData.password = password;

    try {
      setIsLoading(true);
      const updatedUser = await updateUser(id, token, updateData);
      successToast(translate('update_success'));
      console.log('Usuario actualizado:', updatedUser);
      setEmail('');
      setPassword('');
    } catch (error) {
      errorToast(error.message || translate('update_error'));
    } finally {
      setIsLoading(false);
    }
  };

  const handleDeleteAccount = async () => {
    try {
      setIsLoading(true);
      await deleteUser(id, token);
      successToast(translate('account_deleted') || 'Cuenta eliminada exitosamente');
      handleUserLogout();
      navigate('/');
    } catch (error) {
      errorToast(error.message || translate('delete_error') || 'Error al eliminar la cuenta');
    } finally {
      setIsLoading(false);
      setShowDeleteConfirmation(false);
    }
  };

  return (
    <div className="container mt-5">
      <h2>{translate("account_settings")}</h2>
      <hr />

      <Form>
        <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>{translate("change_email")}</Form.Label>
          <Form.Control
            type="email"
            placeholder={translate("new_email")}
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </Form.Group>

        <Form.Group className="mb-3" controlId="formBasicPassword">
          <Form.Label>{translate("change_password")}</Form.Label>
          <Form.Control
            type="password"
            placeholder={translate("new_password")}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </Form.Group>

        <Button
          variant="primary"
          type="button"
          onClick={handleSaveChanges}
          disabled={isLoading}
        >
          {isLoading ? (
            <>
              <Spinner animation="border" size="sm" className="me-2" />
              {translate("saving") || "Guardando..."}
            </>
          ) : (
            translate("save_changes")
          )}
        </Button>
      </Form>

      <hr />

      <div className="danger-zone mt-4">
        <h3>{translate("danger_zone")}</h3>
        <p>{translate("delete_account_warning")}</p>
        <Button variant="danger" onClick={() => setShowDeleteConfirmation(true)}>
          {translate("delete_account")}
        </Button>
      </div>

      <Modal show={showDeleteConfirmation} onHide={() => setShowDeleteConfirmation(false)} centered>
        <Modal.Header closeButton>
          <Modal.Title>{translate("confirm_delete_account")}</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>{translate("sure_to_delete")}</p>
          <p><strong>{translate("action_not_reversible")}</strong></p>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowDeleteConfirmation(false)}>
            {translate("cancel")}
          </Button>
          <Button
            variant="danger"
            onClick={handleDeleteAccount}
            disabled={isLoading}
          >
            {isLoading ? (
              <>
                <Spinner animation="border" size="sm" className="me-2" />
                {translate("deleting") || "Eliminando..."}
              </>
            ) : (
              translate("delete_account")
            )}
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}

export default Settings;
