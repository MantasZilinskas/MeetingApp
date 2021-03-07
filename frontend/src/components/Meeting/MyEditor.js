import React, { useState, useEffect, useRef } from 'react';
import { Box, CircularProgress, makeStyles } from '@material-ui/core';

const useStyles = makeStyles(() => ({
  center: {
    position: 'fixed',
    top: '50%',
    left: '50%',
  },
}));

export default function MyEditor({ editorData, onEditorChange, className }) {
  const editorRef = useRef();
  const [editorLoaded, setEditorLoaded] = useState(false);
  const { CKEditor, ClassicEditor } = editorRef.current || {};

  const classes = useStyles();

  useEffect(() => {
    const { CKEditor } = require('@ckeditor/ckeditor5-react');
    const ClassicEditor = require('@ckeditor/ckeditor5-build-classic');
    editorRef.current = {
      CKEditor: CKEditor,
      ClassicEditor: ClassicEditor,
    };
    setEditorLoaded(true);
    return () => {
      editorRef.current = null;
    };
  }, []);

  return editorLoaded ? (
    <Box className={className}>
      <CKEditor
        editor={ClassicEditor}
        config={{
          toolbar: [
            'heading',
            '|',
            'bold',
            'italic',
            'link',
            'bulletedList',
            'numberedList',
            'blockQuote',
            'insertTable',
            'undo',
            'redo',
          ],
        }}
        data={editorData}
        onChange={onEditorChange}
      />
    </Box>
  ) : (
    <CircularProgress className={classes.center} />
  );
}
