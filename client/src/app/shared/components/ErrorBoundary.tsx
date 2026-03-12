import { Component, ReactNode } from 'react';
import { Box, Button, Container, Paper, Typography } from '@mui/material';
import ErrorOutline from '@mui/icons-material/ErrorOutline';

interface Props {
    children: ReactNode;
}

interface State {
    hasError: boolean;
    error: Error | null;
}

export default class ErrorBoundary extends Component<Props, State> {
    constructor(props: Props) {
        super(props);
        this.state = {
            hasError: false,
            error: null
        };
    }

    static getDerivedStateFromError(error: Error): State {
        return {
            hasError: true,
            error
        };
    }

    componentDidCatch(_error: Error, _errorInfo: React.ErrorInfo) {
        // Log error to error reporting service in production
        // TODO: Send to error tracking service (e.g., Sentry, AppInsights)
    }

    handleReset = () => {
        this.setState({
            hasError: false,
            error: null
        });
        window.location.href = '/';
    };

    render() {
        if (this.state.hasError) {
            return (
                <Container maxWidth="md" sx={{ mt: 8 }}>
                    <Paper
                        elevation={3}
                        sx={{
                            p: 4,
                            textAlign: 'center',
                            backgroundColor: '#fff'
                        }}
                    >
                        <ErrorOutline
                            sx={{
                                fontSize: 80,
                                color: 'error.main',
                                mb: 2
                            }}
                        />
                        <Typography variant="h4" gutterBottom color="error">
                            Oops! Something went wrong
                        </Typography>
                        <Typography variant="body1" color="text.secondary" sx={{ mb: 3 }}>
                            We're sorry for the inconvenience. An unexpected error occurred.
                        </Typography>
                        {this.state.error && (
                            <Box
                                sx={{
                                    mt: 3,
                                    p: 2,
                                    backgroundColor: '#f5f5f5',
                                    borderRadius: 1,
                                    textAlign: 'left',
                                    overflow: 'auto'
                                }}
                            >
                                <Typography variant="subtitle2" color="error" gutterBottom>
                                    Error Details:
                                </Typography>
                                <Typography
                                    variant="body2"
                                    component="pre"
                                    sx={{
                                        fontSize: '0.875rem',
                                        whiteSpace: 'pre-wrap',
                                        wordBreak: 'break-word'
                                    }}
                                >
                                    {this.state.error.toString()}
                                </Typography>
                            </Box>
                        )}
                        <Box sx={{ mt: 4 }}>
                            <Button
                                variant="contained"
                                color="primary"
                                onClick={this.handleReset}
                                size="large"
                            >
                                Return to Home
                            </Button>
                        </Box>
                    </Paper>
                </Container>
            );
        }

        return this.props.children;
    }
}
