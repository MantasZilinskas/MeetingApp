import React from 'react';
import PropTypes from 'prop-types';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TableSortLabel from '@material-ui/core/TableSortLabel';

const EnhancedTableHead = ({ columnOptions, classes, order, orderBy, onRequestSort }) => {
  const headCells = columnOptions;

  const createSortHandler = (property) => (event) => {
    onRequestSort(event, property);
  };

  return (
    <TableHead>
      <TableRow>
        {headCells.map((headCell) => {
          const isSortable = headCell.isSortable === undefined || headCell.isSortable === true;
          const headCellWidth = headCell.width === undefined ? null : headCell.width;

          return (
            <TableCell
              key={headCell.id}
              align={headCell.numeric ? 'right' : 'left'}
              padding={headCell.disablePadding ? 'none' : 'default'}
              sortDirection={orderBy === headCell.id ? order : false}
              style={{ width: headCellWidth }}
            >
              {(isSortable && (
                <TableSortLabel
                  active={orderBy === headCell.label}
                  direction={orderBy === headCell.id ? order : 'asc'}
                  onClick={createSortHandler(headCell.id)}
                >
                  {headCell.label}
                  {orderBy === headCell.label ? (
                    <span className={classes.visuallyHidden}>
                      {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                    </span>
                  ) : null}
                </TableSortLabel>
              )) ||
                headCell.label}
            </TableCell>
          );
        })}
      </TableRow>
    </TableHead>
  );
};

EnhancedTableHead.propTypes = {
  columnOptions: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.string.isRequired,
      label: PropTypes.string.isRequired,
      isSortable: PropTypes.bool,
    })
  ).isRequired,
  classes: PropTypes.object.isRequired,
  onRequestSort: PropTypes.func.isRequired,
  order: PropTypes.oneOf(['asc', 'desc']).isRequired,
  orderBy: PropTypes.string.isRequired,
};

export default EnhancedTableHead;
