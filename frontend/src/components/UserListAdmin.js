//import { makeStyles } from '@material-ui/core/styles';
import React, { useEffect, useState } from 'react';
import { api } from '../axiosInstance';
import ListPage from './ListPage';
import Button from '@material-ui/core/Button';
import { NavLink } from 'react-router-dom';
import GenericTable from './GenericTable';
import GenericModal from './GenericModal';
import { useSnackbar } from 'notistack';

// const useStyles = makeStyles({
//   table: {
//     minWidth: 650,
//   },
// });

export default function UserAdminList() {
  //const classes = useStyles();
  const [users, setUsers] = useState();
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(5);
  const [order, setOrder] = React.useState('asc');
  const [orderBy, setOrderBy] = React.useState('username');
  const [count, setCount] = React.useState(0);
  const [isLoading, setIsLoading] = React.useState(true);
  const [deleteOpen, setDeleteOpen] = useState(false);
  const [deleteId, setDeleteId] = useState();

  const { enqueueSnackbar } = useSnackbar();

  const fetchData = async () => {
    const result = await api.get('user/slice', {
      page,
      rowsPerPage,
      order,
      orderBy,
    });
    setUsers(
      result.items.map((item) => ({
        fullname: item.fullName,
        username: item.userName,
        email: item.email,
        id: item.id,
      }))
    );
    setCount(result.totalCount);
    if (rowsPerPage * page >= result.totalCount) setPage(page - 1);
    setIsLoading(false);
  };
  const handleDeleteAccept = async () => {
    try {
      await api.delete('user/' + deleteId);
      enqueueSnackbar('Request deleted successfully', {
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'center',
        },
        variant: 'success',
      });
      setDeleteOpen(false);
      fetchData();
    } catch (error) {
      console.log(error);
      enqueueSnackbar('Could not delete request', {
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'center',
        },
        variant: 'error',
      });
      setDeleteOpen(false);
    }
  };

  useEffect(() => {
    fetchData();
  }, [order, orderBy, page, rowsPerPage]);

  const userDeleteButton = ({ itemProps }) => {
    return (
      <Button
        color="secondary"
        variant="outlined"
        onClick={() => {
          setDeleteOpen(true);
          setDeleteId(itemProps.id);
        }}
      >
        Delete
      </Button>
    );
  };
  const columnOptions = [
    {
      id: 'username',
      numeric: false,
      disablePadding: false,
      label: 'Username',
    },
    {
      id: 'fullname',
      numeric: false,
      disablePadding: false,
      label: 'Full name',
    },
    { id: 'email', numeric: false, disablePadding: false, label: 'Email' },
    {
      id: 'action',
      numeric: true,
      disablePadding: false,
      label: '',
      ComponentRenderer: userDeleteButton,
      isSortable: false,
    },
  ];
  return (
    <>
      <GenericModal
        handleAccept={handleDeleteAccept}
        handleDecline={() => setDeleteOpen(false)}
        title="Delete"
        description="Are you sure you want to delete this request?"
        isOpen={deleteOpen}
        acceptName="Yes"
        declineName="No"
        handleClose={() => setDeleteOpen(false)}
      ></GenericModal>
      <ListPage
        header="Users"
        controlButtons={
          <Button
            variant="outlined"
            color="primary"
            component={NavLink}
            to={'/user/create'}
          >
            Create user
          </Button>
        }
      >
        <GenericTable
          items={users}
          columnOptions={columnOptions}
          allItemsCount={count}
          setPage={setPage}
          page={page}
          setRowsPerPage={setRowsPerPage}
          rowsPerPage={rowsPerPage}
          setOrder={setOrder}
          order={order}
          setOrderBy={setOrderBy}
          orderBy={orderBy}
          isLoading={isLoading}
        />
      </ListPage>
    </>
  );
}
