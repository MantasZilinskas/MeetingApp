//import { makeStyles } from '@material-ui/core/styles';
import React, { useEffect, useState } from 'react';
import { api } from '../axiosInstance';
import ListPage from './ListPage';
import Button  from '@material-ui/core/Button';
import { NavLink } from 'react-router-dom';
import GenericTable from './GenericTable';

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

  const columnOptions = [
    { id: 'username', numeric: false, disablePadding: false, label: 'Username' },
    { id: 'fullname', numeric: false, disablePadding: false, label: 'Full name' },
    { id: 'email', numeric: false, disablePadding: false, label: 'Email' },
  ];

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
useEffect(() => {
  fetchData();
}, [order, orderBy, page, rowsPerPage]);
  return (
    <ListPage
      header="Users"
      controlButtons={
        <Button variant="outlined" color="primary" component={NavLink} to={'/user/create'}>
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
  );
}
