import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import PropTypes from 'prop-types';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import EnhancedTableHead from './GenericTableHead';
import Skeleton from '@material-ui/lab/Skeleton';

const useStyles = makeStyles((theme) => ({
  root: {
    width: '100%',
  },
  paper: {
    width: '100%',
    marginBottom: theme.spacing(2),
  },
  table: {
    minWidth: 750,
  },
  visuallyHidden: {
    border: 0,
    clip: 'rect(0 0 0 0)',
    height: 1,
    margin: -1,
    overflow: 'hidden',
    padding: 0,
    position: 'absolute',
    top: 20,
    width: 1,
  },
}));

export default function GenericTable({
  allItemsCount,
  items,
  columnOptions,
  page,
  setPage,
  rowsPerPage,
  setRowsPerPage,
  setOrder,
  order,
  orderBy,
  setOrderBy,
  isLoading,
}) {
  const classes = useStyles();

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === 'asc';
    setOrder(isAsc ? 'desc' : 'asc');
    setOrderBy(property);
  };

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeitemsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };
  return isLoading || typeof items === 'undefined' ? (
    <Skeleton animation="wave" variant="rect" height={500} />
  ) : (
    <div className={classes.root}>
      <Paper className={classes.paper} elevation={5}>
        <TableContainer>
          <Table
            className={classes.table}
            aria-labelledby="tableTitle"
            size="medium"
            aria-label="enhanced table"
          >
            <EnhancedTableHead
              columnOptions={columnOptions}
              classes={classes}
              order={order}
              orderBy={orderBy}
              onRequestSort={handleRequestSort}
              rowCount={allItemsCount}
            />
            <TableBody>
              {items.map((item) => {
                return (
                  <TableRow key={item.id}>
                    {columnOptions.map((option) => {
                      if (option.ComponentRenderer != null) {
                        return (
                          <TableCell key={option.id} align={option.numeric ? 'right' : 'left'}>
                            <option.ComponentRenderer optionProps={option} itemProps={item} />
                          </TableCell>
                        );
                      } else
                        return (
                          <TableCell key={option.id} align={option.numeric ? 'right' : 'left'}>
                            {item[option.id]}
                          </TableCell>
                        );
                    })}
                  </TableRow>
                );
              })}
            </TableBody>
          </Table>
        </TableContainer>
        <TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component="div"
          count={allItemsCount}
          rowsPerPage={rowsPerPage}
          page={page}
          onChangePage={handleChangePage}
          onChangeRowsPerPage={handleChangeitemsPerPage}
        />
      </Paper>
    </div>
  );
}

GenericTable.propTypes = {
  order: PropTypes.oneOf(['asc', 'desc']).isRequired,
  orderBy: PropTypes.string.isRequired,
  items: PropTypes.arrayOf(PropTypes.shape({ id: PropTypes.number.isRequired })).isRequired,
  columnOptions: PropTypes.arrayOf(
    PropTypes.shape({ id: PropTypes.string.isRequired, label: PropTypes.string.isRequired })
  ).isRequired,
  setPage: PropTypes.func.isRequired,
  setRowsPerPage: PropTypes.func.isRequired,
  setOrder: PropTypes.func.isRequired,
  setOrderBy: PropTypes.func.isRequired,
  allItemsCount: PropTypes.number,
  rowsPerPage: PropTypes.number.isRequired,
  page: PropTypes.number.isRequired,
  isLoading: PropTypes.bool.isRequired,
};

GenericTable.defaultProps = {
  items: [],
  order: 'asc',
  orderBy: 'date',
  page: 0,
  rowsPerPage: 5,
  allItemsCount: 0,
};
