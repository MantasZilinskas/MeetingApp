//import { makeStyles } from '@material-ui/core/styles';
import React, { useEffect, useState } from 'react';
import { api } from '../../../axiosInstance';
import ListPage from '../../ListPage';
import Button from '@material-ui/core/Button';
import { NavLink } from 'react-router-dom';
import GenericTable from '../../Generic/GenericTable';
import { makeStyles } from '@material-ui/core';
import { currentUserValue } from '../../../Utils/authenticationService';

const useStyles = makeStyles((theme) => ({
  editButton: {
    marginLeft: theme.spacing(1),
  },
}));

export default function UserMeetingList() {
  const classes = useStyles();

  const [meetings, setMeetings] = useState();
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(5);
  const [order, setOrder] = React.useState('asc');
  const [orderBy, setOrderBy] = React.useState('name');
  const [count, setCount] = React.useState(0);
  const [isLoading, setIsLoading] = React.useState(true);
  const currentUser = currentUserValue();

  const userActions = ({ itemProps }) => {
    return (
      <>
        <Button
          className={classes.editButton}
          color="primary"
          variant="outlined"
          component={NavLink}
          to={'/meetingview/' + encodeURI(itemProps.id)}
        >
          More details
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
    const result = await api.get(`user/${currentUser.userId}/meetings/slice`, {
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
  useEffect(() => {
    fetchData();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [order, orderBy, page, rowsPerPage]);
  return (
    <>
      <ListPage header="Meetings">
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
