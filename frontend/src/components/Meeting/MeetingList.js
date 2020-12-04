//import { makeStyles } from '@material-ui/core/styles';
import React, { useEffect, useState } from 'react';
import { api } from '../../axiosInstance';
import ListPage from '../ListPage';
import Button  from '@material-ui/core/Button';
import { NavLink } from 'react-router-dom';
import GenericTable from '../Generic/GenericTable';

// const useStyles = makeStyles({
//   table: {
//     minWidth: 650,
//   },
// });

export default function MeetingList() {
  //const classes = useStyles();
  
  const [meetings, setMeetings] = useState();
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(5);
  const [order, setOrder] = React.useState('asc');
  const [orderBy, setOrderBy] = React.useState('name');
  const [count, setCount] = React.useState(0);
  const [isLoading, setIsLoading] = React.useState(true);

  const columnOptions = [
    { id: 'name', numeric: false, disablePadding: false, label: 'Name' },
    { id: 'description', numeric: false, disablePadding: false, label: 'Description' },
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
useEffect(() => {
  fetchData();
}, [order, orderBy, page, rowsPerPage]);
  return (
    <ListPage
      header="Meetings"
      controlButtons={
        <Button variant="outlined" color="primary" component={NavLink} to={'/meeting/create'}>
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
  );
}
