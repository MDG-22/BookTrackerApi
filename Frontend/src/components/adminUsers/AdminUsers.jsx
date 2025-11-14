import React, { useContext, useEffect, useState } from "react";
import { AuthenticationContext } from "../services/auth/AuthContextProvider.jsx";
import { fetchUsers, updateUserRole, deleteUser } from "./adminUsers.services.js";
import { useNavigate } from "react-router-dom";
import { useTranslate } from "../hooks/translation/UseTranslate";
import { errorToast, infoToast, successToast } from "../notifications/notifications.js";
import { Trash3Fill, CheckLg, XLg, PencilSquare } from "react-bootstrap-icons";
import AdminDeleteModal from "../adminDeleteModal/AdminDeleteModal.jsx";
import { Button, FormSelect, Card, CardHeader } from "react-bootstrap";
import "./adminUsers.css";

const AdminUsers = () => {
  const context = useContext(AuthenticationContext);
  if (!context) return null;

  const { token, role, id: loggedId, updateRole } = context;
  const translate = useTranslate();
  const navigate = useNavigate();

  const [users, setUsers] = useState([]);
  const [selectedUser, setSelectedUser] = useState(null);
  const [showDeleteModal, setShowDeleteModal] = useState(false);
  const [newRole, setNewRole] = useState("");
  const [isEditing, setIsEditing] = useState(false);

  const handleEdit = (user) => {
    setIsEditing(true);
    setSelectedUser(user);
    setNewRole(user.role);
  };

  const handleNewRole = (event) => setNewRole(Number(event.target.value));

  const handleOpenDeleteModal = (user) => {
    setSelectedUser(user);
    setShowDeleteModal(true);
  };

  const handleCloseModal = () => {
    setShowDeleteModal(false);
    setSelectedUser(null);
    setIsEditing(false);
  };

  const handleUpdate = (updatedUser) => {
    setUsers((prev) => prev.map((u) => (u.id === updatedUser.id ? updatedUser : u)));
  };

  const handleDelete = (deletedId) => setUsers((prev) => prev.filter((u) => u.id !== deletedId));

  const handleUpdateUser = async (id) => {
    try {
      if (id === loggedId) updateRole(newRole);

      await updateUserRole(id, token, newRole);

      handleUpdate({ ...selectedUser, role: newRole });

      successToast("Usuario actualizado correctamente");
      setIsEditing(false);
      setSelectedUser(null);

    } catch (error) {
      console.error("Error al actualizar", error);
      errorToast("Error al actualizar");
    }
  };


  const handleDeleteUser = async (id) => {
    try {
      await deleteUser(id, token);
      handleDelete(id);
      handleCloseModal();
      infoToast("Se ha eliminado un usuario");
    } catch (error) {
      console.error("Error al eliminar", error);
    }
  };

  useEffect(() => {
    if (role !== 2) {
      navigate("/");
      return;
    }

    const loadUsers = async () => {
      try {
        const data = await fetchUsers(token);
        setUsers(data);
      } catch (error) {
        console.error("Error fetching users:", error);
        errorToast("Error al cargar los usuarios");
      }
    };

    loadUsers();
  }, [role, token]);

  return (
    <div className="admin-page">
      <div className="admin-container">
        <Card className="admin-grid">
          <CardHeader className="admin-titles id-col">ID</CardHeader>
          <CardHeader className="admin-titles">{translate("username")}</CardHeader>
          <CardHeader className="admin-titles">{translate("email")}</CardHeader>
          <CardHeader className="admin-titles">{translate("role")}</CardHeader>
          <CardHeader className="admin-titles blank-col"></CardHeader>
          <CardHeader className="admin-titles blank-col"></CardHeader>

          {users.map((user) => (
            <React.Fragment key={user.id}>
              <div className="admin-items id-col">{user.id}</div>
              <div className="admin-items">{user.username}</div>
              <div className="admin-items">{user.email}</div>

              {isEditing && selectedUser?.id === user.id ? (
                <FormSelect value={newRole} onChange={handleNewRole}>
                  <option value={0}>{translate("reader")}</option>
                  <option value={1}>{translate("admin")}</option>
                  <option value={2}>{translate("mod")}</option>
                </FormSelect>
              ) : (
                <div className="admin-items">{translate(["reader","admin","mod"][user.role])}</div>
              )}

              <div className="admin-items blank-col">
                {isEditing && selectedUser?.id === user.id ? (
                  <>
                    <Button variant="success" onClick={() => handleUpdateUser(user.id)}>
                      <CheckLg size={20} />
                    </Button>
                    <Button variant="secondary" onClick={handleCloseModal}>
                      <XLg size={20} />
                    </Button>
                  </>
                ) : (
                  <Button variant="secondary" onClick={() => handleEdit(user)}>
                    <PencilSquare size={20} />
                  </Button>
                )}
              </div>

              <div className="admin-items blank-col">
                <Button variant="danger" onClick={() => handleOpenDeleteModal(user)}>
                  <Trash3Fill size={20} />
                </Button>
              </div>
            </React.Fragment>
          ))}
        </Card>
      </div>

      {showDeleteModal && (
        <AdminDeleteModal
          user={selectedUser}
          showDeleteModal={showDeleteModal}
          closeModal={handleCloseModal}
          deleteUser={handleDeleteUser}
        />
      )}
    </div>
  );
};

export default AdminUsers;
