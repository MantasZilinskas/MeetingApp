//import { makeStyles } from '@material-ui/core/styles';
import React, { useEffect, useState } from 'react';
import { api } from '../../axiosInstance';
import ListPage from '../ListPage';
import Button from '@material-ui/core/Button';
import { NavLink } from 'react-router-dom';
import GenericTable from '../Generic/GenericTable';
import { makeStyles } from '@material-ui/core';
import GenericModal from '../Generic/GenericModal';
import { useSnackbar } from 'notistack';

const useStyles = makeStyles((theme) => ({
  editButton: {
    marginLeft: theme.spacing(1),
  },
}));

export default function MeetingList() {
  const classes = useStyles();

  const [meetings, setMeetings] = useState();
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(5);
  const [order, setOrder] = React.useState('asc');
  const [orderBy, setOrderBy] = React.useState('name');
  const [count, setCount] = React.useState(0);
  const [isLoading, setIsLoading] = React.useState(true);
  const [deleteOpen, setDeleteOpen] = useState(false);
  const [deleteId, setDeleteId] = useState();

  const { enqueueSnackbar } = useSnackbar();

  const userActions = ({ itemProps }) => {
    return (
      <>
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
        <Button
          className={classes.editButton}
          color="primary"
          variant="outlined"
          component={NavLink}
          to={'/meeting/' + encodeURI(itemProps.id)}
        >
          Edit
        </Button>
      </>
    );
  };

  const columnOptions = [
    { id: 'name', numeric: false, disablePadding: false, label: 'Name' },
    {
      id: 'description',
      numeric: false,
      disablePadding: false,
      label: 'Description',
    },
    {
      id: 'action',
      numeric: true,
      disablePadding: false,
      label: '',
      ComponentRenderer: userActions,
      isSortable: false,
    },
  ];

  const fetchData = async () => {
    const result = await api.get('meeting/slice', {
      page,
      rowsPerPage,
      order,
      orderBy,
    });

    setMeetings(
      result.items.map((item) => ({
        name: item.name,
        description: item.description,
        id: item.id,
      }))
    );
    setCount(result.totalCount);
    if (rowsPerPage * page >= result.totalCount) setPage(page - 1);
    setIsLoading(false);
  };
  const handleDeleteAccept = async () => {
    try {
      await api.delete('meeting/' + deleteId);
      enqueueSnackbar('Meeting deleted successfully', {
        anchorOrigin: {
          vertical: 'bottom',
          horizontal: 'center',
        },
        variant: 'success',
      });
      setDeleteOpen(false);
      fetchData();
    } catch (error) {
      enqueueSnackbar(error.message, {
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
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [order, orderBy, page, rowsPerPage]);
  return (
    <>
      <GenericModal
        handleAccept={handleDeleteAccept}
        handleDecline={() => setDeleteOpen(false)}
        title="Delete"
        description="Are you sure you want to delete this user?"
        isOpen={deleteOpen}
        acceptName="Yes"
        declineName="No"
        handleClose={() => setDeleteOpen(false)}
      ></GenericModal>
      <ListPage
        header="Meetings"
        controlButtons={
          <Button
            variant="outlined"
            color="primary"
            component={NavLink}
            to={'/meeting/create'}
          >
            Create meeting
          </Button>
        }
      >
        <GenericTable
          items={meetings}
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
